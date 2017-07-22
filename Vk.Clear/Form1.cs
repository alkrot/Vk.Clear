using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Vk.Clear
{
    public class Form1 : Form
	{
		private delegate void LabelText(string text);

	    private delegate void BtnEnabled(bool btnEnabled);

	
		private readonly Work _work = new Work();


		private Button _btnGroupsLeave;

		private Label _label1;

		private Button _btnDeleteFriends;

		private Button _btnDeleteWall;

		private Button _btnDeletePhotos;

		private Button _btnDeleteVideo;

		private Button _btnDeleteAudio;

		private Button _btnDeleteDialogs;

		private Button _btnDeleteNewsfeed;

		private Button _btnGroupsUnban;

		private Button _btnAccountBanned;

		private TabControl _tabControl1;

		private TabPage _tabPage1;

		private TabPage _tabPage2;

		private TabPage _tabPage3;

		private Button _btnDeleteTopic;

		private Button _btnDeleteNotes;

		private Button _btnDeleteDocs;

		private Button _btnDeleteFollowers;

		private Button _btnDeleteMembers;

		private Button _btnDeletedDieUsers;

		public bool ButtonEnabled => _tabControl1.Enabled;

	    public Form1()
		{
            InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            _work.Start(5274836, "groups,friends,wall,photos,video,audio,messages,notes,docs,offline");
            _work.AddForms(this);
			if (_work.ID == 0)
			{
				MessageBox.Show("Подключите инет или дайте доступ приложению");
                Close();
			}
            _work.StatsTrackVisitor();
            _label1.Text = "Твой id: " + _work.ID;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.Groups);
			thread.Start();
		}

		public void SetButtonEnabled(bool btnEnabled)
		{
			if (InvokeRequired)
			{
                BeginInvoke(new BtnEnabled(SetButtonEnabled), btnEnabled);
				return;
			}
            _tabControl1.Enabled = btnEnabled;
		}

		public void SetTextLabel(string text)
		{
			if (InvokeRequired)
			{
                BeginInvoke(new LabelText(SetTextLabel), text);
				return;
			}
            _label1.Text = text;
		}

		private void btnDeleteFriends_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.Friends);
			thread.Start();
		}

		private void btnDeleteWall_Click(object sender, EventArgs e)
		{
			string text = _work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != _work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(_work.Wall);
				thread.Start(text);
			}
		}

		public static DialogResult InputBox(string title, string promptText, ref string value)
		{
			Form form = new Form();
			Label label = new Label();
			TextBox textBox = new TextBox();
			Button button = new Button();
			form.Text = title;
			label.Text = promptText;
			textBox.Text = value;
			button.Text = "OK";
			label.SetBounds(9, 20, 372, 13);
			textBox.SetBounds(12, 36, 372, 20);
			button.SetBounds(228, 72, 75, 23);
			button.DialogResult = DialogResult.OK;
			label.AutoSize = true;
			textBox.Anchor |= AnchorStyles.Right;
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			form.ClientSize = new Size(396, 107);
			form.Controls.AddRange(new Control[]
			{
				label,
				textBox,
				button
			});
			form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = button;
			DialogResult result = form.ShowDialog();
			value = textBox.Text;
			return result;
		}

		public static DialogResult ShowBox(string title, string promptText)
		{
			Form form = new Form();
			Label label = new Label();
			Button button = new Button();
			Button button2 = new Button();
			form.Text = title;
			label.Text = promptText;
			button.Text = "Да";
			button2.Text = "Нет";
			label.SetBounds(9, 20, 372, 13);
			button.SetBounds(100, 72, 75, 23);
			button2.SetBounds(300, 72, 75, 23);
			button.DialogResult = DialogResult.OK;
			button2.DialogResult = DialogResult.Cancel;
			label.AutoSize = true;
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			form.ClientSize = new Size(396, 107);
			form.Controls.AddRange(new Control[]
			{
				label,
				button,
				button2
			});
			form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = button;
			return form.ShowDialog();
		}

		private void btnDeletePhotos_Click(object sender, EventArgs e)
		{
			string text = _work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				if (DialogResult.OK == ShowBox("Внимание", "Удалить альбомы с фотографиями?"))
				{
					text = ((text != _work.ID.ToString()) ? ("-" + text) : text);
					Thread thread = new Thread(_work.PhotosAlbum);
					thread.Start(text);
					return;
				}
				if (text.Length >= 0 && text == _work.ID.ToString() && DialogResult.OK == ShowBox("Внимание", "Удалить и сохраненые фотографии?"))
				{
                    _work.DelSavePhoto = true;
				}
				text = ((text != _work.ID.ToString()) ? ("-" + text) : text);
				if (text != _work.ID.ToString() && DialogResult.OK == ShowBox("Внимание", "Удалить фотографии со стены сообщества?"))
				{
                    _work.DelWallPhotoGroup = true;
				}
				Thread thread2 = new Thread(_work.Photos);
				thread2.Start(text);
			}
		}

		private void btnDeleteVideo_Click(object sender, EventArgs e)
		{
			string text = _work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				if (DialogResult.OK == ShowBox("Внимание", "Удалить и альбомы?"))
				{
                    _work.DelAlbumVideo = true;
				}
				text = ((text != _work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(_work.Video);
				thread.Start(text);
			}
		}

		private void btnDeleteAudio_Click(object sender, EventArgs e)
		{
			string text = _work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != _work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(_work.Audio);
				thread.Start(text);
			}
		}

		private void btnDeleteDialogs_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.Messages);
			thread.Start();
		}

		private void btnDeleteNewsfeed_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.NewsfeedLists);
			thread.Start();
		}

		private void btnGroupsUnban_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				Thread thread = new Thread(_work.GroupsBanned);
				thread.Start(text);
			}
		}

		private void btnAccountBanned_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.AccountBanned);
			thread.Start();
		}

		private void btnDeleteTopic_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				int num = Math.Abs(int.Parse(text));
				Thread thread = new Thread(_work.BoardTopics);
				thread.Start(num);
			}
		}

		private void btnDeleteNotes_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.Notes);
			thread.Start();
		}

		private void btnDeleteDocs_Click(object sender, EventArgs e)
		{
			string text = _work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != _work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(_work.Docs);
				thread.Start(text);
			}
		}

		private void OutVk()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
			string[] files = Directory.GetFiles(folderPath);
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string path = array[i];
				try
				{
					File.Delete(path);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_work.ID > 0 && DialogResult.OK == ShowBox("Внимание", "Выйти с аккаунта?"))
			{
                OutVk();
			}
		}

		private void btnDeleteFollowers_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(_work.Followers);
			thread.Start();
		}

		private void btnDeleteMembers_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				int num = Math.Abs(int.Parse(text));
				Thread thread = new Thread(_work.GroupsMembers);
				thread.Start(num);
			}
		}

		private void btnDeletedDieUsers_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text))
			{
				if (DialogResult.OK == ShowBox("Внимание", "Удалить и заблокированных?"))
				{
                    _work.DelBanned = true;
				}
				if (text.Length > 0)
				{
					int num = Math.Abs(int.Parse(text));
					Thread thread = new Thread(_work.GroupsMembersDeleted);
					thread.Start(num);
				}
			}
		}

		private void InitializeComponent()
		{
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            _btnGroupsLeave = new Button();
            _label1 = new Label();
            _btnDeleteFriends = new Button();
            _btnDeleteWall = new Button();
            _btnDeletePhotos = new Button();
            _btnDeleteVideo = new Button();
            _btnDeleteAudio = new Button();
            _btnDeleteDialogs = new Button();
            _btnDeleteNewsfeed = new Button();
            _btnGroupsUnban = new Button();
            _btnAccountBanned = new Button();
            _tabControl1 = new TabControl();
            _tabPage1 = new TabPage();
            _btnDeleteDocs = new Button();
            _tabPage2 = new TabPage();
            _btnDeleteFollowers = new Button();
            _btnDeleteNotes = new Button();
            _tabPage3 = new TabPage();
            _btnDeletedDieUsers = new Button();
            _btnDeleteMembers = new Button();
            _btnDeleteTopic = new Button();
            _tabControl1.SuspendLayout();
            _tabPage1.SuspendLayout();
            _tabPage2.SuspendLayout();
            _tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // btnGroupsLeave
            // 
            _btnGroupsLeave.Location = new Point(6, 122);
            _btnGroupsLeave.Name = "_btnGroupsLeave";
            _btnGroupsLeave.Size = new Size(157, 23);
            _btnGroupsLeave.TabIndex = 0;
            _btnGroupsLeave.Text = "Выйти из всех групп";
            _btnGroupsLeave.UseVisualStyleBackColor = true;
            _btnGroupsLeave.Click += button1_Click;
            // 
            // label1
            // 
            _label1.Dock = DockStyle.Bottom;
            _label1.Location = new Point(0, 236);
            _label1.Name = "_label1";
            _label1.Size = new Size(226, 33);
            _label1.TabIndex = 1;
            _label1.Text = "Информация\r\nИнформация";
            _label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDeleteFriends
            // 
            _btnDeleteFriends.Location = new Point(6, 64);
            _btnDeleteFriends.Name = "_btnDeleteFriends";
            _btnDeleteFriends.Size = new Size(157, 23);
            _btnDeleteFriends.TabIndex = 2;
            _btnDeleteFriends.Text = "Удалить всех друзей";
            _btnDeleteFriends.UseVisualStyleBackColor = true;
            _btnDeleteFriends.Click += btnDeleteFriends_Click;
            // 
            // btnDeleteWall
            // 
            _btnDeleteWall.Location = new Point(6, 92);
            _btnDeleteWall.Name = "_btnDeleteWall";
            _btnDeleteWall.Size = new Size(158, 23);
            _btnDeleteWall.TabIndex = 3;
            _btnDeleteWall.Text = "Очистить стену";
            _btnDeleteWall.UseVisualStyleBackColor = true;
            _btnDeleteWall.Click += btnDeleteWall_Click;
            // 
            // btnDeletePhotos
            // 
            _btnDeletePhotos.Location = new Point(6, 34);
            _btnDeletePhotos.Name = "_btnDeletePhotos";
            _btnDeletePhotos.Size = new Size(158, 23);
            _btnDeletePhotos.TabIndex = 4;
            _btnDeletePhotos.Text = "Удалить все фотографии";
            _btnDeletePhotos.UseVisualStyleBackColor = true;
            _btnDeletePhotos.Click += btnDeletePhotos_Click;
            // 
            // btnDeleteVideo
            // 
            _btnDeleteVideo.Location = new Point(6, 6);
            _btnDeleteVideo.Name = "_btnDeleteVideo";
            _btnDeleteVideo.Size = new Size(158, 22);
            _btnDeleteVideo.TabIndex = 5;
            _btnDeleteVideo.Text = "Удалить все видео";
            _btnDeleteVideo.UseVisualStyleBackColor = true;
            _btnDeleteVideo.Click += btnDeleteVideo_Click;
            // 
            // btnDeleteAudio
            // 
            _btnDeleteAudio.Enabled = false;
            _btnDeleteAudio.Location = new Point(6, 63);
            _btnDeleteAudio.Name = "_btnDeleteAudio";
            _btnDeleteAudio.Size = new Size(158, 23);
            _btnDeleteAudio.TabIndex = 6;
            _btnDeleteAudio.Text = "Удалить все аудиозаписи";
            _btnDeleteAudio.UseVisualStyleBackColor = true;
            _btnDeleteAudio.Click += btnDeleteAudio_Click;
            // 
            // btnDeleteDialogs
            // 
            _btnDeleteDialogs.Location = new Point(6, 6);
            _btnDeleteDialogs.Name = "_btnDeleteDialogs";
            _btnDeleteDialogs.Size = new Size(157, 23);
            _btnDeleteDialogs.TabIndex = 7;
            _btnDeleteDialogs.Text = "Удалить все сообщения";
            _btnDeleteDialogs.UseVisualStyleBackColor = true;
            _btnDeleteDialogs.Click += btnDeleteDialogs_Click;
            // 
            // btnDeleteNewsfeed
            // 
            _btnDeleteNewsfeed.Location = new Point(6, 35);
            _btnDeleteNewsfeed.Name = "_btnDeleteNewsfeed";
            _btnDeleteNewsfeed.Size = new Size(157, 23);
            _btnDeleteNewsfeed.TabIndex = 8;
            _btnDeleteNewsfeed.Text = "Удалить список новостей";
            _btnDeleteNewsfeed.UseVisualStyleBackColor = true;
            _btnDeleteNewsfeed.Click += btnDeleteNewsfeed_Click;
            // 
            // btnGroupsUnban
            // 
            _btnGroupsUnban.Location = new Point(6, 6);
            _btnGroupsUnban.Name = "_btnGroupsUnban";
            _btnGroupsUnban.Size = new Size(157, 23);
            _btnGroupsUnban.TabIndex = 9;
            _btnGroupsUnban.Text = "Разбанить всех в группе";
            _btnGroupsUnban.UseVisualStyleBackColor = true;
            _btnGroupsUnban.Click += btnGroupsUnban_Click;
            // 
            // btnAccountBanned
            // 
            _btnAccountBanned.Location = new Point(6, 93);
            _btnAccountBanned.Name = "_btnAccountBanned";
            _btnAccountBanned.Size = new Size(157, 23);
            _btnAccountBanned.TabIndex = 10;
            _btnAccountBanned.Text = "Очистить черный список";
            _btnAccountBanned.UseVisualStyleBackColor = true;
            _btnAccountBanned.Click += btnAccountBanned_Click;
            // 
            // tabControl1
            // 
            _tabControl1.Controls.Add(_tabPage1);
            _tabControl1.Controls.Add(_tabPage2);
            _tabControl1.Controls.Add(_tabPage3);
            _tabControl1.Dock = DockStyle.Fill;
            _tabControl1.Location = new Point(0, 0);
            _tabControl1.Name = "_tabControl1";
            _tabControl1.SelectedIndex = 0;
            _tabControl1.Size = new Size(226, 236);
            _tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            _tabPage1.Controls.Add(_btnDeleteDocs);
            _tabPage1.Controls.Add(_btnDeletePhotos);
            _tabPage1.Controls.Add(_btnDeleteVideo);
            _tabPage1.Controls.Add(_btnDeleteAudio);
            _tabPage1.Controls.Add(_btnDeleteWall);
            _tabPage1.Location = new Point(4, 22);
            _tabPage1.Name = "_tabPage1";
            _tabPage1.Padding = new Padding(3);
            _tabPage1.Size = new Size(218, 210);
            _tabPage1.TabIndex = 0;
            _tabPage1.Text = "Общие";
            _tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDocs
            // 
            _btnDeleteDocs.Location = new Point(6, 121);
            _btnDeleteDocs.Name = "_btnDeleteDocs";
            _btnDeleteDocs.Size = new Size(158, 23);
            _btnDeleteDocs.TabIndex = 7;
            _btnDeleteDocs.Text = "Удалить все документы";
            _btnDeleteDocs.UseVisualStyleBackColor = true;
            _btnDeleteDocs.Click += btnDeleteDocs_Click;
            // 
            // tabPage2
            // 
            _tabPage2.Controls.Add(_btnDeleteFollowers);
            _tabPage2.Controls.Add(_btnDeleteNotes);
            _tabPage2.Controls.Add(_btnDeleteDialogs);
            _tabPage2.Controls.Add(_btnGroupsLeave);
            _tabPage2.Controls.Add(_btnAccountBanned);
            _tabPage2.Controls.Add(_btnDeleteNewsfeed);
            _tabPage2.Controls.Add(_btnDeleteFriends);
            _tabPage2.Location = new Point(4, 22);
            _tabPage2.Name = "_tabPage2";
            _tabPage2.Padding = new Padding(3);
            _tabPage2.Size = new Size(218, 210);
            _tabPage2.TabIndex = 1;
            _tabPage2.Text = "Пользователь";
            _tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteFollowers
            // 
            _btnDeleteFollowers.Location = new Point(8, 178);
            _btnDeleteFollowers.Name = "_btnDeleteFollowers";
            _btnDeleteFollowers.Size = new Size(155, 23);
            _btnDeleteFollowers.TabIndex = 12;
            _btnDeleteFollowers.Text = "Удалить подписчиков";
            _btnDeleteFollowers.UseVisualStyleBackColor = true;
            _btnDeleteFollowers.Click += btnDeleteFollowers_Click;
            // 
            // btnDeleteNotes
            // 
            _btnDeleteNotes.Location = new Point(6, 151);
            _btnDeleteNotes.Name = "_btnDeleteNotes";
            _btnDeleteNotes.Size = new Size(157, 23);
            _btnDeleteNotes.TabIndex = 11;
            _btnDeleteNotes.Text = "Удалить все заметки";
            _btnDeleteNotes.UseVisualStyleBackColor = true;
            _btnDeleteNotes.Click += btnDeleteNotes_Click;
            // 
            // tabPage3
            // 
            _tabPage3.Controls.Add(_btnDeletedDieUsers);
            _tabPage3.Controls.Add(_btnDeleteMembers);
            _tabPage3.Controls.Add(_btnDeleteTopic);
            _tabPage3.Controls.Add(_btnGroupsUnban);
            _tabPage3.Location = new Point(4, 22);
            _tabPage3.Name = "_tabPage3";
            _tabPage3.Padding = new Padding(3);
            _tabPage3.Size = new Size(218, 210);
            _tabPage3.TabIndex = 2;
            _tabPage3.Text = "Группа";
            _tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDeletedDieUsers
            // 
            _btnDeletedDieUsers.Location = new Point(8, 93);
            _btnDeletedDieUsers.Name = "_btnDeletedDieUsers";
            _btnDeletedDieUsers.Size = new Size(155, 23);
            _btnDeletedDieUsers.TabIndex = 12;
            _btnDeletedDieUsers.Text = "Удалить мертвых";
            _btnDeletedDieUsers.UseVisualStyleBackColor = true;
            _btnDeletedDieUsers.Click += btnDeletedDieUsers_Click;
            // 
            // btnDeleteMembers
            // 
            _btnDeleteMembers.Location = new Point(8, 64);
            _btnDeleteMembers.Name = "_btnDeleteMembers";
            _btnDeleteMembers.Size = new Size(155, 23);
            _btnDeleteMembers.TabIndex = 11;
            _btnDeleteMembers.Text = "Удалить участников";
            _btnDeleteMembers.UseVisualStyleBackColor = true;
            _btnDeleteMembers.Click += btnDeleteMembers_Click;
            // 
            // btnDeleteTopic
            // 
            _btnDeleteTopic.Location = new Point(6, 35);
            _btnDeleteTopic.Name = "_btnDeleteTopic";
            _btnDeleteTopic.Size = new Size(157, 23);
            _btnDeleteTopic.TabIndex = 10;
            _btnDeleteTopic.Text = "Удалить все обсуждения";
            _btnDeleteTopic.UseVisualStyleBackColor = true;
            _btnDeleteTopic.Click += btnDeleteTopic_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(226, 269);
            Controls.Add(_tabControl1);
            Controls.Add(_label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            MaximizeBox = false;
            Name = "Form1";
            Text = "Vk.Clear";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            _tabControl1.ResumeLayout(false);
            _tabPage1.ResumeLayout(false);
            _tabPage2.ResumeLayout(false);
            _tabPage3.ResumeLayout(false);
            ResumeLayout(false);

		}
	}
}
