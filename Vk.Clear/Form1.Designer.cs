using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Vk.Clear
{
    partial class Form1
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
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
    }
}