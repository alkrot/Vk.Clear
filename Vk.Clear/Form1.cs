using System;
using System.Threading;
using System.Windows.Forms;

namespace Vk.Clear
{
    /// <summary>
    /// Main form
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initialize form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            work.Start(5274836, "groups,friends,wall,photos,video,audio,messages,notes,docs,offline");
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
            thread = new Thread(work.Groups);
            thread.Start();
        }

        private void btnDeleteFriends_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.Friends);
            thread.Start();
        }

        private void btnDeleteWall_Click(object sender, EventArgs e)
        {
            string text = work.ID.ToString();
            if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
            {
                text = ((text != work.ID.ToString()) ? ("-" + text) : text);
                thread = new Thread(work.Wall);
                thread.Start(text);
            }
        }

        private void btnDeletePhotos_Click(object sender, EventArgs e)
        {
            string text = work.ID.ToString();
            if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
            {
                if (DialogResult.OK == ShowBox("Внимание", "Удалить альбомы с фотографиями?"))
                {
                    text = ((text != work.ID.ToString()) ? ("-" + text) : text);
                    thread = new Thread(work.PhotosAlbum);
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
                thread = new Thread(work.Photos);
                thread.Start(text);
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
                thread = new Thread(work.Video);
                thread.Start(text);
            }
        }

        private void btnDeleteAudio_Click(object sender, EventArgs e)
        {
            string text = work.ID.ToString();
            if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
            {
                text = ((text != work.ID.ToString()) ? ("-" + text) : text);
                thread = new Thread(work.Audio);
                thread.Start(text);
            }
        }

        private void btnDeleteDialogs_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.Messages);
            thread.Start();
        }

        private void btnDeleteNewsfeed_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.NewsfeedLists);
            thread.Start();
        }

        private void btnGroupsUnban_Click(object sender, EventArgs e)
        {
            string text = "";
            if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
            {
                thread = new Thread(work.GroupsBanned);
                thread.Start(text);
            }
        }

        private void btnAccountBanned_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.AccountBanned);
            thread.Start();
        }

        private void btnDeleteTopic_Click(object sender, EventArgs e)
        {
            string text = "";
            if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
            {
                int num = Math.Abs(int.Parse(text));
                thread = new Thread(work.BoardTopics);
                thread.Start(num);
            }
        }

        private void btnDeleteNotes_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.Notes);
            thread.Start();
        }

        private void btnDeleteDocs_Click(object sender, EventArgs e)
        {
            string text = work.ID.ToString();
            if (DialogResult.OK == InputBox("Введите id группы или оставьте свой", "id", ref text))
            {
                text = ((text != work.ID.ToString()) ? ("-" + text) : text);
                thread = new Thread(work.Docs);
                thread.Start(text);
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
            thread = new Thread(work.Followers);
            thread.Start();
        }

        private void btnDeleteMembers_Click(object sender, EventArgs e)
        {
            string text = "";
            if (DialogResult.OK == InputBox("Введите id группы", "id", ref text) && text.Length > 0)
            {
                int num = Math.Abs(int.Parse(text));
                thread = new Thread(work.GroupsMembers);
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
                    thread = new Thread(work.GroupsMembersDeleted);
                    thread.Start(num);
                }
            }
        }

        private void btnDeleteLikePhoto_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.FavePhotos);
            thread.Start();
        }

        private void btnDeleteLikeVideo_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.FaveVideo);
            thread.Start();
        }

        private void btnDeleteLikePost_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.FavePost);
            thread.Start(100);
        }

        private void btnRemoveFaveUsers_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.FaveUsers);
            thread.Start();
        }

        private void btnRemoveLink_Click(object sender, EventArgs e)
        {
            thread = new Thread(work.FaveLink);
            thread.Start();
        }

        private void остановитьЗаданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (thread != null && (thread.IsAlive || thread.ThreadState == ThreadState.Running))
            {
                SetButtonEnabled(true);
            }
        }
    }
}
