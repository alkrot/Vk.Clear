using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Vk.Clear
{
    public partial class Form1 : Form
	{
		private delegate void LabelText(string text);

	    private delegate void BtnEnabled(bool btnEnabled);

	
		private readonly Work work = new Work();

		public bool ButtonEnabled => tabControl1.Enabled;

	    public Form1()
		{
            InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            work.Start(5274836,"groups,friends,wall,photos,video,audio,messages,notes,docs,offline");
            work.AddForms(this);
			if (work.ID == 0)
			{
				MessageBox.Show(@"Подключите инет или дайте доступ приложению");
                Close();
			}
            work.StatsTrackVisitor();
            _label1.Text = @"Твой id: " + work.ID;
		}

		private void btnOutGroup_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(work.Groups);
			thread.Start();
		}

		public void SetButtonEnabled(bool btnEnabled)
		{
			if (InvokeRequired)
			{
                BeginInvoke(new BtnEnabled(SetButtonEnabled), btnEnabled);
				return;
			}
            tabControl1.Enabled = btnEnabled;
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
			Thread thread = new Thread(work.Friends);
			thread.Start();
		}

		private void btnDeleteWall_Click(object sender, EventArgs e)
		{
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(work.Wall);
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
			button.Text = @"OK";
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
			button.Text = @"Да";
			button2.Text = @"Нет";
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
					Thread thread = new Thread(work.PhotosAlbum);
					thread.Start(text);
					return;
				}
				if (text.Length >= 0 && text == work.ID.ToString() && DialogResult.OK == ShowBox("Внимание", "Удалить и сохраненые фотографии?"))
				{
                    work.DelSavePhoto = true;
				}
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				if (text != work.ID.ToString() && DialogResult.OK == ShowBox("Внимание", "Удалить фотографии со стены сообщества?"))
				{
                    work.DelWallPhotoGroup = true;
				}
				Thread thread2 = new Thread(work.Photos);
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
				Thread thread = new Thread(work.Video);
				thread.Start(text);
			}
		}

		private void btnDeleteAudio_Click(object sender, EventArgs e)
		{
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(work.Audio);
				thread.Start(text);
			}
		}

		private void btnDeleteDialogs_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(work.Messages);
			thread.Start();
		}

		private void btnDeleteNewsfeed_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(work.NewsfeedLists);
			thread.Start();
		}

		private void btnGroupsUnban_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				Thread thread = new Thread(work.GroupsBanned);
				thread.Start(text);
			}
		}

		private void btnAccountBanned_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(work.AccountBanned);
			thread.Start();
		}

		private void btnDeleteTopic_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				int num = Math.Abs(int.Parse(text));
				Thread thread = new Thread(work.BoardTopics);
				thread.Start(num);
			}
		}

		private void btnDeleteNotes_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(work.Notes);
			thread.Start();
		}

		private void btnDeleteDocs_Click(object sender, EventArgs e)
		{
			string text = work.ID.ToString();
			if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
			{
				text = ((text != work.ID.ToString()) ? ("-" + text) : text);
				Thread thread = new Thread(work.Docs);
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
			if (work.ID > 0 && DialogResult.OK == ShowBox("Внимание", "Выйти с аккаунта?"))
			{
                OutVk();
			}
		}

		private void btnDeleteFollowers_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(work.Followers);
			thread.Start();
		}

		private void btnDeleteMembers_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
			{
				int num = Math.Abs(int.Parse(text));
				Thread thread = new Thread(work.GroupsMembers);
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
					Thread thread = new Thread(work.GroupsMembersDeleted);
					thread.Start(num);
				}
			}
		}

        private void btnDeleteLikePhoto_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(work.favePhotos);
            th.Start();
        }

        private void btnDeleteLikeVideo_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(work.faveVideo);
            th.Start();
        }

        private void btnDeleteLikePost_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(work.favePost);
            th.Start();
        }

        private void btnRemoveFaveUsers_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(work.faveUsers);
            th.Start();
        }

        private void btnRemoveLink_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(work.faveLink);
            th.Start();
        }
    }
}
