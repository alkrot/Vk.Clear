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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGroupsLeave = new System.Windows.Forms.Button();
            this._label1 = new System.Windows.Forms.Label();
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
            this._tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDeleteDocs = new System.Windows.Forms.Button();
            this._tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRemoveLink = new System.Windows.Forms.Button();
            this.btnRemoveFaveUsers = new System.Windows.Forms.Button();
            this.btnDeleteLikePost = new System.Windows.Forms.Button();
            this.btnDeleteLikeVideo = new System.Windows.Forms.Button();
            this.btnDeleteLikePhoto = new System.Windows.Forms.Button();
            this.btnDeleteFollowers = new System.Windows.Forms.Button();
            this.btnDeleteNotes = new System.Windows.Forms.Button();
            this._tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDeletedDieUsers = new System.Windows.Forms.Button();
            this.btnDeleteMembers = new System.Windows.Forms.Button();
            this.btnDeleteTopic = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this._tabPage1.SuspendLayout();
            this._tabPage2.SuspendLayout();
            this._tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGroupsLeave
            // 
            this.btnGroupsLeave.Location = new System.Drawing.Point(6, 122);
            this.btnGroupsLeave.Name = "btnGroupsLeave";
            this.btnGroupsLeave.Size = new System.Drawing.Size(204, 23);
            this.btnGroupsLeave.TabIndex = 0;
            this.btnGroupsLeave.Text = "Выйти из всех групп";
            this.btnGroupsLeave.UseVisualStyleBackColor = true;
            this.btnGroupsLeave.Click += new System.EventHandler(this.btnOutGroup_Click);
            // 
            // _label1
            // 
            this._label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._label1.Location = new System.Drawing.Point(0, 233);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(245, 33);
            this._label1.TabIndex = 1;
            this._label1.Text = "Информация\r\nИнформация";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteFriends
            // 
            this.btnDeleteFriends.Location = new System.Drawing.Point(6, 64);
            this.btnDeleteFriends.Name = "btnDeleteFriends";
            this.btnDeleteFriends.Size = new System.Drawing.Size(204, 23);
            this.btnDeleteFriends.TabIndex = 2;
            this.btnDeleteFriends.Text = "Удалить всех друзей";
            this.btnDeleteFriends.UseVisualStyleBackColor = true;
            this.btnDeleteFriends.Click += new System.EventHandler(this.btnDeleteFriends_Click);
            // 
            // btnDeleteWall
            // 
            this.btnDeleteWall.Location = new System.Drawing.Point(6, 92);
            this.btnDeleteWall.Name = "btnDeleteWall";
            this.btnDeleteWall.Size = new System.Drawing.Size(223, 23);
            this.btnDeleteWall.TabIndex = 3;
            this.btnDeleteWall.Text = "Очистить стену";
            this.btnDeleteWall.UseVisualStyleBackColor = true;
            this.btnDeleteWall.Click += new System.EventHandler(this.btnDeleteWall_Click);
            // 
            // btnDeletePhotos
            // 
            this.btnDeletePhotos.Location = new System.Drawing.Point(6, 34);
            this.btnDeletePhotos.Name = "btnDeletePhotos";
            this.btnDeletePhotos.Size = new System.Drawing.Size(223, 23);
            this.btnDeletePhotos.TabIndex = 4;
            this.btnDeletePhotos.Text = "Удалить все фотографии";
            this.btnDeletePhotos.UseVisualStyleBackColor = true;
            this.btnDeletePhotos.Click += new System.EventHandler(this.btnDeletePhotos_Click);
            // 
            // btnDeleteVideo
            // 
            this.btnDeleteVideo.Location = new System.Drawing.Point(6, 6);
            this.btnDeleteVideo.Name = "btnDeleteVideo";
            this.btnDeleteVideo.Size = new System.Drawing.Size(223, 22);
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
            this.btnDeleteAudio.Size = new System.Drawing.Size(223, 23);
            this.btnDeleteAudio.TabIndex = 6;
            this.btnDeleteAudio.Text = "Удалить все аудиозаписи";
            this.btnDeleteAudio.UseVisualStyleBackColor = true;
            this.btnDeleteAudio.Click += new System.EventHandler(this.btnDeleteAudio_Click);
            // 
            // btnDeleteDialogs
            // 
            this.btnDeleteDialogs.Location = new System.Drawing.Point(6, 6);
            this.btnDeleteDialogs.Name = "btnDeleteDialogs";
            this.btnDeleteDialogs.Size = new System.Drawing.Size(204, 23);
            this.btnDeleteDialogs.TabIndex = 7;
            this.btnDeleteDialogs.Text = "Удалить все сообщения";
            this.btnDeleteDialogs.UseVisualStyleBackColor = true;
            this.btnDeleteDialogs.Click += new System.EventHandler(this.btnDeleteDialogs_Click);
            // 
            // btnDeleteNewsfeed
            // 
            this.btnDeleteNewsfeed.Location = new System.Drawing.Point(6, 35);
            this.btnDeleteNewsfeed.Name = "btnDeleteNewsfeed";
            this.btnDeleteNewsfeed.Size = new System.Drawing.Size(204, 23);
            this.btnDeleteNewsfeed.TabIndex = 8;
            this.btnDeleteNewsfeed.Text = "Удалить список новостей";
            this.btnDeleteNewsfeed.UseVisualStyleBackColor = true;
            this.btnDeleteNewsfeed.Click += new System.EventHandler(this.btnDeleteNewsfeed_Click);
            // 
            // btnGroupsUnban
            // 
            this.btnGroupsUnban.Location = new System.Drawing.Point(6, 6);
            this.btnGroupsUnban.Name = "btnGroupsUnban";
            this.btnGroupsUnban.Size = new System.Drawing.Size(223, 23);
            this.btnGroupsUnban.TabIndex = 9;
            this.btnGroupsUnban.Text = "Разбанить всех в группе";
            this.btnGroupsUnban.UseVisualStyleBackColor = true;
            this.btnGroupsUnban.Click += new System.EventHandler(this.btnGroupsUnban_Click);
            // 
            // btnAccountBanned
            // 
            this.btnAccountBanned.Location = new System.Drawing.Point(6, 93);
            this.btnAccountBanned.Name = "btnAccountBanned";
            this.btnAccountBanned.Size = new System.Drawing.Size(204, 23);
            this.btnAccountBanned.TabIndex = 10;
            this.btnAccountBanned.Text = "Очистить черный список";
            this.btnAccountBanned.UseVisualStyleBackColor = true;
            this.btnAccountBanned.Click += new System.EventHandler(this.btnAccountBanned_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._tabPage1);
            this.tabControl1.Controls.Add(this._tabPage2);
            this.tabControl1.Controls.Add(this._tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(245, 233);
            this.tabControl1.TabIndex = 11;
            // 
            // _tabPage1
            // 
            this._tabPage1.Controls.Add(this.btnDeleteDocs);
            this._tabPage1.Controls.Add(this.btnDeletePhotos);
            this._tabPage1.Controls.Add(this.btnDeleteVideo);
            this._tabPage1.Controls.Add(this.btnDeleteAudio);
            this._tabPage1.Controls.Add(this.btnDeleteWall);
            this._tabPage1.Location = new System.Drawing.Point(4, 22);
            this._tabPage1.Name = "_tabPage1";
            this._tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this._tabPage1.Size = new System.Drawing.Size(237, 207);
            this._tabPage1.TabIndex = 0;
            this._tabPage1.Text = "Общие";
            this._tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDocs
            // 
            this.btnDeleteDocs.Location = new System.Drawing.Point(6, 121);
            this.btnDeleteDocs.Name = "btnDeleteDocs";
            this.btnDeleteDocs.Size = new System.Drawing.Size(223, 23);
            this.btnDeleteDocs.TabIndex = 7;
            this.btnDeleteDocs.Text = "Удалить все документы";
            this.btnDeleteDocs.UseVisualStyleBackColor = true;
            this.btnDeleteDocs.Click += new System.EventHandler(this.btnDeleteDocs_Click);
            // 
            // _tabPage2
            // 
            this._tabPage2.AutoScroll = true;
            this._tabPage2.Controls.Add(this.btnRemoveLink);
            this._tabPage2.Controls.Add(this.btnRemoveFaveUsers);
            this._tabPage2.Controls.Add(this.btnDeleteLikePost);
            this._tabPage2.Controls.Add(this.btnDeleteLikeVideo);
            this._tabPage2.Controls.Add(this.btnDeleteLikePhoto);
            this._tabPage2.Controls.Add(this.btnDeleteFollowers);
            this._tabPage2.Controls.Add(this.btnDeleteNotes);
            this._tabPage2.Controls.Add(this.btnDeleteDialogs);
            this._tabPage2.Controls.Add(this.btnGroupsLeave);
            this._tabPage2.Controls.Add(this.btnAccountBanned);
            this._tabPage2.Controls.Add(this.btnDeleteNewsfeed);
            this._tabPage2.Controls.Add(this.btnDeleteFriends);
            this._tabPage2.Location = new System.Drawing.Point(4, 22);
            this._tabPage2.Name = "_tabPage2";
            this._tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this._tabPage2.Size = new System.Drawing.Size(237, 207);
            this._tabPage2.TabIndex = 1;
            this._tabPage2.Text = "Пользователь";
            this._tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRemoveLink
            // 
            this.btnRemoveLink.Location = new System.Drawing.Point(8, 323);
            this.btnRemoveLink.Name = "btnRemoveLink";
            this.btnRemoveLink.Size = new System.Drawing.Size(204, 23);
            this.btnRemoveLink.TabIndex = 17;
            this.btnRemoveLink.Text = "Удалить ссылки из закладок";
            this.btnRemoveLink.UseVisualStyleBackColor = true;
            this.btnRemoveLink.Click += new System.EventHandler(this.btnRemoveLink_Click);
            // 
            // btnRemoveFaveUsers
            // 
            this.btnRemoveFaveUsers.Location = new System.Drawing.Point(6, 294);
            this.btnRemoveFaveUsers.Name = "btnRemoveFaveUsers";
            this.btnRemoveFaveUsers.Size = new System.Drawing.Size(204, 23);
            this.btnRemoveFaveUsers.TabIndex = 16;
            this.btnRemoveFaveUsers.Text = "Удалить людей из закладок";
            this.btnRemoveFaveUsers.UseVisualStyleBackColor = true;
            this.btnRemoveFaveUsers.Click += new System.EventHandler(this.btnRemoveFaveUsers_Click);
            // 
            // btnDeleteLikePost
            // 
            this.btnDeleteLikePost.Location = new System.Drawing.Point(8, 265);
            this.btnDeleteLikePost.Name = "btnDeleteLikePost";
            this.btnDeleteLikePost.Size = new System.Drawing.Size(204, 23);
            this.btnDeleteLikePost.TabIndex = 15;
            this.btnDeleteLikePost.Text = "Удалить лайки с постов";
            this.btnDeleteLikePost.UseVisualStyleBackColor = true;
            this.btnDeleteLikePost.Click += new System.EventHandler(this.btnDeleteLikePost_Click);
            // 
            // btnDeleteLikeVideo
            // 
            this.btnDeleteLikeVideo.Location = new System.Drawing.Point(6, 236);
            this.btnDeleteLikeVideo.Name = "btnDeleteLikeVideo";
            this.btnDeleteLikeVideo.Size = new System.Drawing.Size(204, 23);
            this.btnDeleteLikeVideo.TabIndex = 14;
            this.btnDeleteLikeVideo.Text = "Удалить лайки с видео";
            this.btnDeleteLikeVideo.UseVisualStyleBackColor = true;
            this.btnDeleteLikeVideo.Click += new System.EventHandler(this.btnDeleteLikeVideo_Click);
            // 
            // btnDeleteLikePhoto
            // 
            this.btnDeleteLikePhoto.Location = new System.Drawing.Point(8, 207);
            this.btnDeleteLikePhoto.Name = "btnDeleteLikePhoto";
            this.btnDeleteLikePhoto.Size = new System.Drawing.Size(202, 23);
            this.btnDeleteLikePhoto.TabIndex = 13;
            this.btnDeleteLikePhoto.Text = "Удалить лайки с фотографий";
            this.btnDeleteLikePhoto.UseVisualStyleBackColor = true;
            this.btnDeleteLikePhoto.Click += new System.EventHandler(this.btnDeleteLikePhoto_Click);
            // 
            // btnDeleteFollowers
            // 
            this.btnDeleteFollowers.Location = new System.Drawing.Point(8, 178);
            this.btnDeleteFollowers.Name = "btnDeleteFollowers";
            this.btnDeleteFollowers.Size = new System.Drawing.Size(202, 23);
            this.btnDeleteFollowers.TabIndex = 12;
            this.btnDeleteFollowers.Text = "Удалить подписчиков";
            this.btnDeleteFollowers.UseVisualStyleBackColor = true;
            this.btnDeleteFollowers.Click += new System.EventHandler(this.btnDeleteFollowers_Click);
            // 
            // btnDeleteNotes
            // 
            this.btnDeleteNotes.Location = new System.Drawing.Point(6, 151);
            this.btnDeleteNotes.Name = "btnDeleteNotes";
            this.btnDeleteNotes.Size = new System.Drawing.Size(204, 23);
            this.btnDeleteNotes.TabIndex = 11;
            this.btnDeleteNotes.Text = "Удалить все заметки";
            this.btnDeleteNotes.UseVisualStyleBackColor = true;
            this.btnDeleteNotes.Click += new System.EventHandler(this.btnDeleteNotes_Click);
            // 
            // _tabPage3
            // 
            this._tabPage3.Controls.Add(this.btnDeletedDieUsers);
            this._tabPage3.Controls.Add(this.btnDeleteMembers);
            this._tabPage3.Controls.Add(this.btnDeleteTopic);
            this._tabPage3.Controls.Add(this.btnGroupsUnban);
            this._tabPage3.Location = new System.Drawing.Point(4, 22);
            this._tabPage3.Name = "_tabPage3";
            this._tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this._tabPage3.Size = new System.Drawing.Size(237, 207);
            this._tabPage3.TabIndex = 2;
            this._tabPage3.Text = "Группа";
            this._tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDeletedDieUsers
            // 
            this.btnDeletedDieUsers.Location = new System.Drawing.Point(8, 93);
            this.btnDeletedDieUsers.Name = "btnDeletedDieUsers";
            this.btnDeletedDieUsers.Size = new System.Drawing.Size(221, 23);
            this.btnDeletedDieUsers.TabIndex = 12;
            this.btnDeletedDieUsers.Text = "Удалить мертвых";
            this.btnDeletedDieUsers.UseVisualStyleBackColor = true;
            this.btnDeletedDieUsers.Click += new System.EventHandler(this.btnDeletedDieUsers_Click);
            // 
            // btnDeleteMembers
            // 
            this.btnDeleteMembers.Location = new System.Drawing.Point(8, 64);
            this.btnDeleteMembers.Name = "btnDeleteMembers";
            this.btnDeleteMembers.Size = new System.Drawing.Size(221, 23);
            this.btnDeleteMembers.TabIndex = 11;
            this.btnDeleteMembers.Text = "Удалить участников";
            this.btnDeleteMembers.UseVisualStyleBackColor = true;
            this.btnDeleteMembers.Click += new System.EventHandler(this.btnDeleteMembers_Click);
            // 
            // btnDeleteTopic
            // 
            this.btnDeleteTopic.Location = new System.Drawing.Point(6, 35);
            this.btnDeleteTopic.Name = "btnDeleteTopic";
            this.btnDeleteTopic.Size = new System.Drawing.Size(223, 23);
            this.btnDeleteTopic.TabIndex = 10;
            this.btnDeleteTopic.Text = "Удалить все обсуждения";
            this.btnDeleteTopic.UseVisualStyleBackColor = true;
            this.btnDeleteTopic.Click += new System.EventHandler(this.btnDeleteTopic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 266);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Vk.Clear";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this._tabPage1.ResumeLayout(false);
            this._tabPage2.ResumeLayout(false);
            this._tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Button btnGroupsLeave;

        private Label _label1;

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

        private TabPage _tabPage1;

        private TabPage _tabPage2;

        private TabPage _tabPage3;

        private Button btnDeleteTopic;

        private Button btnDeleteNotes;

        private Button btnDeleteDocs;

        private Button btnDeleteFollowers;

        private Button btnDeleteMembers;

        private Button btnDeletedDieUsers;
        private Button btnDeleteLikePhoto;
        private Button btnDeleteLikeVideo;
        private Button btnDeleteLikePost;
        private Button btnRemoveFaveUsers;
        private Button btnRemoveLink;
    }
}