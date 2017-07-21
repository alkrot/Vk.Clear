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

		private delegate void buttonEnabled(bool btnEnabled);

	
		private Work work = new Work();


		private Button btnGroupsLeave;

		private Label label1;

		private Button btnDeleteFriends;

		private Button btnDeleteWall;

		private Button btnDeletePhotos;

		private Button btnDeleteVideo;

		private Button btnDeleteAudio;

		private Button btnDeleteDialogs;

		private Button btnDeleteNewsfeed;

		private Button btnGroupsUnban;

		private Button btnAccountBanned;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private Button btnDeleteTopic;

		private Button btnDeleteNotes;

		private Button btnDeleteDocs;

		private Button btnDeleteFollowers;

		private Button btnDeleteMembers;

		private Button btnDeletedDieUsers;

		public bool ButtonEnabled
		{
			get
			{
				return tabControl1.Enabled;
			}
		}

		public Form1()
		{
            InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            work.Start(5274836, "groups,friends,wall,photos,video,audio,messages,notes,docs,offline");
            work.addForms(this);
			if (work.ID == 0)
			{
				MessageBox.Show("Подключите инет или дайте доступ приложению");
                Close();
			}
            work.statsTrackVisitor();
            label1.Text = "Твой id: " + work.ID;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.groups));
			thread.Start();
		}

		public void setButtonEnabled(bool btnEnabled)
		{
			if (InvokeRequired)
			{
                BeginInvoke(new buttonEnabled(this.setButtonEnabled), new object[]
				{
					btnEnabled
				});
				return;
			}
            tabControl1.Enabled = btnEnabled;
		}

		public void setTextLabel(string text)
		{
			if (InvokeRequired)
			{
                BeginInvoke(new LabelText(this.setTextLabel), new object[]
				{
					text
				});
				return;
			}
            label1.Text = text;
		}

		private void btnDeleteFriends_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.friends));
			thread.Start();
		}

		private void btnDeleteWall_Click(object sender, EventArgs e)
		{
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(new ParameterizedThreadStart(work.wall));
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
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				if (DialogResult.OK == ShowBox("Внимание", "Удалить альбомы с фотографиями?"))
				{
					text = ((text != work.ID.ToString()) ? ("-" + text) : text);
					Thread thread = new Thread(new ParameterizedThreadStart(work.photosAlbum));
					thread.Start(text);
					return;
				}
				if (text.Length >= 0 && text == this.work.ID.ToString() && DialogResult.OK == ShowBox("Внимание", "Удалить и сохраненые фотографии?"))
				{
                    work.DelSavePhoto = true;
				}
				text = ((text != this.work.ID.ToString()) ? ("-" + text) : text);
				if (text != this.work.ID.ToString() && DialogResult.OK == ShowBox("Внимание", "Удалить фотографии со стены сообщества?"))
				{
                    work.DelWallPhotoGroup = true;
				}
				Thread thread2 = new Thread(new ParameterizedThreadStart(work.photos));
				thread2.Start(text);
			}
		}

		private void btnDeleteVideo_Click(object sender, EventArgs e)
		{
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				if (DialogResult.OK == ShowBox("Внимание", "Удалить и альбомы?"))
				{
                    work.DelAlbumVideo = true;
				}
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(new ParameterizedThreadStart(work.video));
				thread.Start(text);
			}
		}

		private void btnDeleteAudio_Click(object sender, EventArgs e)
		{
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(new ParameterizedThreadStart(work.audio));
				thread.Start(text);
			}
		}

		private void btnDeleteDialogs_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.messages));
			thread.Start();
		}

		private void btnDeleteNewsfeed_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.newsfeedLists));
			thread.Start();
		}

		private void btnGroupsUnban_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				Thread thread = new Thread(new ParameterizedThreadStart(work.groupsBanned));
				thread.Start(text);
			}
		}

		private void btnAccountBanned_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.accountBanned));
			thread.Start();
		}

		private void btnDeleteTopic_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				int num = Math.Abs(int.Parse(text));
				Thread thread = new Thread(new ParameterizedThreadStart(work.boardTopics));
				thread.Start(num);
			}
		}

		private void btnDeleteNotes_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.notes));
			thread.Start();
		}

		private void btnDeleteDocs_Click(object sender, EventArgs e)
		{
			string text = this.work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(new ParameterizedThreadStart(work.docs));
				thread.Start(text);
			}
		}

		private void outVk()
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
			if (work.ID > 0 && DialogResult.OK == ShowBox("Внимание", "Выйти с аккаунта?"))
			{
                outVk();
			}
		}

		private void btnDeleteFollowers_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(work.followers));
			thread.Start();
		}

		private void btnDeleteMembers_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				int num = Math.Abs(int.Parse(text));
				Thread thread = new Thread(new ParameterizedThreadStart(work.groupsMembers));
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
                    work.DelBanned = true;
				}
				if (text.Length > 0)
				{
					int num = Math.Abs(int.Parse(text));
					Thread thread = new Thread(new ParameterizedThreadStart(work.groupsMembersDeleted));
					thread.Start(num);
				}
			}
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGroupsLeave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteFriends = new System.Windows.Forms.Button();
            this.btnDeleteWall = new System.Windows.Forms.Button();
            this.btnDeletePhotos = new System.Windows.Forms.Button();
            this.btnDeleteVideo = new System.Windows.Forms.Button();
            this.btnDeleteAudio = new System.Windows.Forms.Button();
            this.btnDeleteDialogs = new System.Windows.Forms.Button();
            this.btnDeleteNewsfeed = new System.Windows.Forms.Button();
            this.btnGroupsUnban = new System.Windows.Forms.Button();
            this.btnAccountBanned = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDeleteDocs = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteFollowers = new System.Windows.Forms.Button();
            this.btnDeleteNotes = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDeletedDieUsers = new System.Windows.Forms.Button();
            this.btnDeleteMembers = new System.Windows.Forms.Button();
            this.btnDeleteTopic = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGroupsLeave
            // 
            this.btnGroupsLeave.Location = new System.Drawing.Point(6, 122);
            this.btnGroupsLeave.Name = "btnGroupsLeave";
            this.btnGroupsLeave.Size = new System.Drawing.Size(157, 23);
            this.btnGroupsLeave.TabIndex = 0;
            this.btnGroupsLeave.Text = "Выйти из всех групп";
            this.btnGroupsLeave.UseVisualStyleBackColor = true;
            this.btnGroupsLeave.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Информация\r\nИнформация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteFriends
            // 
            this.btnDeleteFriends.Location = new System.Drawing.Point(6, 64);
            this.btnDeleteFriends.Name = "btnDeleteFriends";
            this.btnDeleteFriends.Size = new System.Drawing.Size(157, 23);
            this.btnDeleteFriends.TabIndex = 2;
            this.btnDeleteFriends.Text = "Удалить всех друзей";
            this.btnDeleteFriends.UseVisualStyleBackColor = true;
            this.btnDeleteFriends.Click += new System.EventHandler(this.btnDeleteFriends_Click);
            // 
            // btnDeleteWall
            // 
            this.btnDeleteWall.Location = new System.Drawing.Point(6, 92);
            this.btnDeleteWall.Name = "btnDeleteWall";
            this.btnDeleteWall.Size = new System.Drawing.Size(158, 23);
            this.btnDeleteWall.TabIndex = 3;
            this.btnDeleteWall.Text = "Очистить стену";
            this.btnDeleteWall.UseVisualStyleBackColor = true;
            this.btnDeleteWall.Click += new System.EventHandler(this.btnDeleteWall_Click);
            // 
            // btnDeletePhotos
            // 
            this.btnDeletePhotos.Location = new System.Drawing.Point(6, 34);
            this.btnDeletePhotos.Name = "btnDeletePhotos";
            this.btnDeletePhotos.Size = new System.Drawing.Size(158, 23);
            this.btnDeletePhotos.TabIndex = 4;
            this.btnDeletePhotos.Text = "Удалить все фотографии";
            this.btnDeletePhotos.UseVisualStyleBackColor = true;
            this.btnDeletePhotos.Click += new System.EventHandler(this.btnDeletePhotos_Click);
            // 
            // btnDeleteVideo
            // 
            this.btnDeleteVideo.Location = new System.Drawing.Point(6, 6);
            this.btnDeleteVideo.Name = "btnDeleteVideo";
            this.btnDeleteVideo.Size = new System.Drawing.Size(158, 22);
            this.btnDeleteVideo.TabIndex = 5;
            this.btnDeleteVideo.Text = "Удалить все видео";
            this.btnDeleteVideo.UseVisualStyleBackColor = true;
            this.btnDeleteVideo.Click += new System.EventHandler(this.btnDeleteVideo_Click);
            // 
            // btnDeleteAudio
            // 
            this.btnDeleteAudio.Enabled = false;
            this.btnDeleteAudio.Location = new System.Drawing.Point(6, 63);
            this.btnDeleteAudio.Name = "btnDeleteAudio";
            this.btnDeleteAudio.Size = new System.Drawing.Size(158, 23);
            this.btnDeleteAudio.TabIndex = 6;
            this.btnDeleteAudio.Text = "Удалить все аудиозаписи";
            this.btnDeleteAudio.UseVisualStyleBackColor = true;
            this.btnDeleteAudio.Click += new System.EventHandler(this.btnDeleteAudio_Click);
            // 
            // btnDeleteDialogs
            // 
            this.btnDeleteDialogs.Location = new System.Drawing.Point(6, 6);
            this.btnDeleteDialogs.Name = "btnDeleteDialogs";
            this.btnDeleteDialogs.Size = new System.Drawing.Size(157, 23);
            this.btnDeleteDialogs.TabIndex = 7;
            this.btnDeleteDialogs.Text = "Удалить все сообщения";
            this.btnDeleteDialogs.UseVisualStyleBackColor = true;
            this.btnDeleteDialogs.Click += new System.EventHandler(this.btnDeleteDialogs_Click);
            // 
            // btnDeleteNewsfeed
            // 
            this.btnDeleteNewsfeed.Location = new System.Drawing.Point(6, 35);
            this.btnDeleteNewsfeed.Name = "btnDeleteNewsfeed";
            this.btnDeleteNewsfeed.Size = new System.Drawing.Size(157, 23);
            this.btnDeleteNewsfeed.TabIndex = 8;
            this.btnDeleteNewsfeed.Text = "Удалить список новостей";
            this.btnDeleteNewsfeed.UseVisualStyleBackColor = true;
            this.btnDeleteNewsfeed.Click += new System.EventHandler(this.btnDeleteNewsfeed_Click);
            // 
            // btnGroupsUnban
            // 
            this.btnGroupsUnban.Location = new System.Drawing.Point(6, 6);
            this.btnGroupsUnban.Name = "btnGroupsUnban";
            this.btnGroupsUnban.Size = new System.Drawing.Size(157, 23);
            this.btnGroupsUnban.TabIndex = 9;
            this.btnGroupsUnban.Text = "Разбанить всех в группе";
            this.btnGroupsUnban.UseVisualStyleBackColor = true;
            this.btnGroupsUnban.Click += new System.EventHandler(this.btnGroupsUnban_Click);
            // 
            // btnAccountBanned
            // 
            this.btnAccountBanned.Location = new System.Drawing.Point(6, 93);
            this.btnAccountBanned.Name = "btnAccountBanned";
            this.btnAccountBanned.Size = new System.Drawing.Size(157, 23);
            this.btnAccountBanned.TabIndex = 10;
            this.btnAccountBanned.Text = "Очистить черный список";
            this.btnAccountBanned.UseVisualStyleBackColor = true;
            this.btnAccountBanned.Click += new System.EventHandler(this.btnAccountBanned_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(226, 236);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDeleteDocs);
            this.tabPage1.Controls.Add(this.btnDeletePhotos);
            this.tabPage1.Controls.Add(this.btnDeleteVideo);
            this.tabPage1.Controls.Add(this.btnDeleteAudio);
            this.tabPage1.Controls.Add(this.btnDeleteWall);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(218, 210);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Общие";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDocs
            // 
            this.btnDeleteDocs.Location = new System.Drawing.Point(6, 121);
            this.btnDeleteDocs.Name = "btnDeleteDocs";
            this.btnDeleteDocs.Size = new System.Drawing.Size(158, 23);
            this.btnDeleteDocs.TabIndex = 7;
            this.btnDeleteDocs.Text = "Удалить все документы";
            this.btnDeleteDocs.UseVisualStyleBackColor = true;
            this.btnDeleteDocs.Click += new System.EventHandler(this.btnDeleteDocs_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteFollowers);
            this.tabPage2.Controls.Add(this.btnDeleteNotes);
            this.tabPage2.Controls.Add(this.btnDeleteDialogs);
            this.tabPage2.Controls.Add(this.btnGroupsLeave);
            this.tabPage2.Controls.Add(this.btnAccountBanned);
            this.tabPage2.Controls.Add(this.btnDeleteNewsfeed);
            this.tabPage2.Controls.Add(this.btnDeleteFriends);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(218, 210);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Пользователь";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteFollowers
            // 
            this.btnDeleteFollowers.Location = new System.Drawing.Point(8, 178);
            this.btnDeleteFollowers.Name = "btnDeleteFollowers";
            this.btnDeleteFollowers.Size = new System.Drawing.Size(155, 23);
            this.btnDeleteFollowers.TabIndex = 12;
            this.btnDeleteFollowers.Text = "Удалить подписчиков";
            this.btnDeleteFollowers.UseVisualStyleBackColor = true;
            this.btnDeleteFollowers.Click += new System.EventHandler(this.btnDeleteFollowers_Click);
            // 
            // btnDeleteNotes
            // 
            this.btnDeleteNotes.Location = new System.Drawing.Point(6, 151);
            this.btnDeleteNotes.Name = "btnDeleteNotes";
            this.btnDeleteNotes.Size = new System.Drawing.Size(157, 23);
            this.btnDeleteNotes.TabIndex = 11;
            this.btnDeleteNotes.Text = "Удалить все заметки";
            this.btnDeleteNotes.UseVisualStyleBackColor = true;
            this.btnDeleteNotes.Click += new System.EventHandler(this.btnDeleteNotes_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDeletedDieUsers);
            this.tabPage3.Controls.Add(this.btnDeleteMembers);
            this.tabPage3.Controls.Add(this.btnDeleteTopic);
            this.tabPage3.Controls.Add(this.btnGroupsUnban);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(218, 210);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Группа";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDeletedDieUsers
            // 
            this.btnDeletedDieUsers.Location = new System.Drawing.Point(8, 93);
            this.btnDeletedDieUsers.Name = "btnDeletedDieUsers";
            this.btnDeletedDieUsers.Size = new System.Drawing.Size(155, 23);
            this.btnDeletedDieUsers.TabIndex = 12;
            this.btnDeletedDieUsers.Text = "Удалить мертвых";
            this.btnDeletedDieUsers.UseVisualStyleBackColor = true;
            this.btnDeletedDieUsers.Click += new System.EventHandler(this.btnDeletedDieUsers_Click);
            // 
            // btnDeleteMembers
            // 
            this.btnDeleteMembers.Location = new System.Drawing.Point(8, 64);
            this.btnDeleteMembers.Name = "btnDeleteMembers";
            this.btnDeleteMembers.Size = new System.Drawing.Size(155, 23);
            this.btnDeleteMembers.TabIndex = 11;
            this.btnDeleteMembers.Text = "Удалить участников";
            this.btnDeleteMembers.UseVisualStyleBackColor = true;
            this.btnDeleteMembers.Click += new System.EventHandler(this.btnDeleteMembers_Click);
            // 
            // btnDeleteTopic
            // 
            this.btnDeleteTopic.Location = new System.Drawing.Point(6, 35);
            this.btnDeleteTopic.Name = "btnDeleteTopic";
            this.btnDeleteTopic.Size = new System.Drawing.Size(157, 23);
            this.btnDeleteTopic.TabIndex = 10;
            this.btnDeleteTopic.Text = "Удалить все обсуждения";
            this.btnDeleteTopic.UseVisualStyleBackColor = true;
            this.btnDeleteTopic.Click += new System.EventHandler(this.btnDeleteTopic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 269);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Vk.Clear";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}
