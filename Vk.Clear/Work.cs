using System;
using System.Collections.Generic;
using System.Threading;
using VkApi;
using Newtonsoft.Json.Linq;

namespace Vk.Clear
{
    public class Work : ApiVk
    {
        private string v = "5.44";

        private Form1 form;

        private int wallCount;

        private int photoCount;

        private int videoCount;

        private int messagesCount;

        private int bannedCount;

        private int boardTopicsCount;

        private int notesCount;

        private int followersCount;

        private string manager = "";

        private int managerCount;

        private int i;

        private bool delSavePhoto;

        private bool delAlbumVideo;

        private bool delBanned;

        private bool delWallPhotoGroup;

        public bool DelSavePhoto
        {
            get
            {
                return this.delSavePhoto;
            }
            set
            {
                this.delSavePhoto = value;
            }
        }

        public bool DelAlbumVideo
        {
            get
            {
                return this.delAlbumVideo;
            }
            set
            {
                this.delAlbumVideo = value;
            }
        }

        public bool DelBanned
        {
            get
            {
                return this.delBanned;
            }
            set
            {
                this.delBanned = value;
            }
        }

        public bool DelWallPhotoGroup
        {
            get
            {
                return this.delWallPhotoGroup;
            }
            set
            {
                this.delWallPhotoGroup = value;
            }
        }

        public Work()
        {
        }

        public Work(string _v)
        {
            this.v = _v;
        }

        public void statsTrackVisitor()
        {
            base.Send("stats.trackVisitor", "", this.v);
        }

        public void addForms(Form1 form)
        {
            this.form = form;
        }

        public void groups()
        {
            this.enabledButton(false);
            JObject jObject = base.Send("groups.get", "", this.v);
            int num = jObject["response"]["count"].Value<int>();
            foreach (JToken current in ((IEnumerable<JToken>)jObject["response"]["items"]))
            {
                int arg_79_1 = current.Value<int>();
                int num2 = this.i;
                this.i = num2 + 1;
                this.leaveGroups(arg_79_1, num2, num);
            }
            if (num > 0)
            {
                this.groups();
            }
            this.enabledButton(true);
            this.writeLog("Вышли");
            this.i = 0;
        }

        public void groupsBanned(object group_id)
        {
            this.enabledButton(false);
            int num = Math.Abs(int.Parse(group_id.ToString()));
            JObject jObject = base.Send("groups.getBanned", "group_id=" + num + "&count=200", this.v);
            int num2 = jObject["response"]["count"].Value<int>();
            this.bannedCount = ((this.bannedCount == 0) ? num2 : this.bannedCount);
            num2 = ((num2 > 200) ? 200 : num2);
            for (int i = 0; i < num2; i++)
            {
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_D5_1 = num;
                int arg_D5_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.groupsUnbanUser(arg_D5_1, arg_D5_2, num4, this.bannedCount);
            }
            if (num2 > 0)
            {
                this.groupsBanned(group_id);
            }
            this.i = 0;
            this.bannedCount = 0;
            this.writeLog("Пользователи в группе разбаненны");
            this.enabledButton(true);
        }

        public void friends()
        {
            this.enabledButton(false);
            JObject jObject = base.Send("friends.get", "", this.v);
            int count = jObject["response"]["count"].Value<int>();
            foreach (JToken current in ((IEnumerable<JToken>)jObject["response"]["items"]))
            {
                int arg_79_1 = current.Value<int>();
                int num = this.i;
                this.i = num + 1;
                this.friendsDelete(arg_79_1, num, count);
            }
            this.writeLog("У Вас, больше нет друзей");
            this.i = 0;
            this.enabledButton(true);
        }

        public void wall(object owner_id)
        {
            this.enabledButton(false);
            JObject jObject = base.Send("wall.get", "owner_id=" + owner_id + "&count=100", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.wallCount = ((this.wallCount == 0) ? num : this.wallCount);
            num = ((num >= 100) ? 100 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["id"].Value<int>();
                int num3 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int arg_EC_1 = num3;
                int arg_EC_2 = num2;
                int num4 = this.i;
                this.i = num4 + 1;
                this.wallDelete(arg_EC_1, arg_EC_2, num4, this.wallCount);
            }
            if (num > 0)
            {
                this.wall(owner_id);
            }
            this.wallCount = 0;
            this.writeLog("Чистка стены, закончена");
            this.i = 0;
            this.enabledButton(true);
        }

        public void photos(object owner_id)
        {
            this.enabledButton(false);
            JObject jObject = base.Send("photos.getAll", "owner_id=" + owner_id + "&count=200", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.photoCount = ((this.photoCount == 0) ? num : this.photoCount);
            num = ((num >= 200) ? 200 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_F7_1 = num2;
                int arg_F7_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.photoDelete(arg_F7_1, arg_F7_2, num4, this.photoCount, "Удалено фотографий ");
            }
            if (num > 0)
            {
                this.photos(owner_id);
            }
            this.writeLog("Фотографии удаленны");
            this.i = 0;
            this.photoCount = 0;
            if (this.delSavePhoto && owner_id.ToString() == base.ID.ToString())
            {
                this.writeLog("Начинаем парсить...");
                this.photosSaved();
            }
            else if (this.DelWallPhotoGroup)
            {
                this.writeLog("Начинаем парсить...");
                this.photosWall(owner_id.ToString());
            }
            this.enabledButton(true);
        }

        private void photosWall(string owner_id)
        {
            Thread.Sleep(1000);
            JObject jObject = base.Send("photos.get", "owner_id=" + owner_id + "&album_id=wall&count=200&rev=1", "5.44");
            int num = jObject["response"]["count"].Value<int>();
            this.photoCount = ((this.photoCount == 0) ? num : this.photoCount);
            num = ((num > 200) ? 200 : num);
            for (int i = 1; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_F9_1 = num2;
                int arg_F9_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.photoDelete(arg_F9_1, arg_F9_2, num4, this.photoCount, "Удалено фотографий ");
            }
            if (num > 0 && this.i < 1000)
            {
                this.photosWall(owner_id);
            }
            this.i = 0;
            this.photoCount = 0;
            this.writeLog("Удалили (максимум 1000) за раз");
        }

        public void photosAlbum(object owner_id)
        {
            this.enabledButton(false);
            JObject jObject = base.Send("photos.getAlbums", "owner_id=" + owner_id, this.v);
            int num = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_C0_1 = num2;
                int arg_C0_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.photosAlbumDelete(arg_C0_1, arg_C0_2, num4, num);
            }
            this.writeLog("Альбомы удалены");
            this.i = 0;
            this.enabledButton(true);
        }

        public void video(object owner_id)
        {
            this.enabledButton(false);
            this.writeLog("Получаем видео");
            Thread.Sleep(500);
            JObject jObject = base.Send("video.get", "owner_id=" + owner_id + "&count=200", this.v);
            JArray jArray = jObject["response"]["items"].Value<JArray>();
            int num = jObject["response"]["count"].Value<int>();
            this.videoCount = ((this.videoCount == 0) ? num : this.videoCount);
            for (int i = 0; i < jArray.Count; i++)
            {
                int num2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int num4 = int.Parse(owner_id.ToString());
                int arg_122_1 = num3;
                int arg_122_2 = num2;
                int arg_122_3 = num4;
                int num5 = this.i;
                this.i = num5 + 1;
                this.videoDelete(arg_122_1, arg_122_2, arg_122_3, num5, this.videoCount);
            }
            if (num > 0)
            {
                this.video(owner_id);
            }
            this.writeLog("Видео удаленны");
            this.videoCount = 0;
            this.i = 0;
            if (this.delAlbumVideo)
            {
                this.videoAlbum(owner_id);
            }
            this.enabledButton(true);
        }

        public void audio(object owner_id)
        {
            this.enabledButton(false);
            this.writeLog("Удаление аудиозаписей не работает");
            this.enabledButton(true);
        }

        public void messages()
        {
            this.enabledButton(false);
            JObject jObject = base.Send("messages.getDialogs", "count=200", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.messagesCount = ((this.messagesCount == 0) ? num : this.messagesCount);
            num = ((num > 200) ? 200 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = (jObject["response"]["items"][i]["message"]["user_id"] ?? -1).Value<int>();
                int num3 = (jObject["response"]["items"][i]["message"]["chat_id"] ?? -1).Value<int>();
                int arg_10F_1 = num2;
                int arg_10F_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.messagesDelete(arg_10F_1, arg_10F_2, num4, this.messagesCount);
            }
            if (num > 0)
            {
                this.messages();
            }
            this.i = 0;
            this.messagesCount = 0;
            this.writeLog("Все диалоги удалены");
            this.enabledButton(true);
        }

        public void newsfeedLists()
        {
            if (this.form.ButtonEnabled)
            {
                this.enabledButton(false);
            }
            JObject jObject = base.Send("newsfeed.getLists", "", this.v);
            int num = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < num; i++)
            {
                int list_id = jObject["response"]["items"][i]["id"].Value<int>();
                this.newsfeedDelete(list_id, i, num);
            }
            this.writeLog("Все списки удалены");
            this.enabledButton(true);
        }

        public void accountBanned()
        {
            this.enabledButton(false);
            JObject jObject = base.Send("account.getBanned", "count=200", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.bannedCount = ((this.bannedCount == 0) ? num : this.bannedCount);
            num = ((num > 200) ? 200 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_B1_1 = num2;
                int num3 = this.i;
                this.i = num3 + 1;
                this.accountUnban(arg_B1_1, num3, this.bannedCount);
            }
            if (num > 0)
            {
                this.accountBanned();
            }
            this.i = 0;
            this.bannedCount = 0;
            this.writeLog("Ваш черный список чист");
            this.enabledButton(true);
        }

        public void boardTopics(object group_id)
        {
            this.enabledButton(false);
            JObject jObject = base.Send("board.getTopics", "group_id=" + group_id + "&count=100", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.boardTopicsCount = ((this.boardTopicsCount == 0) ? num : this.boardTopicsCount);
            num = ((num > 100) ? 100 : num);
            int num2 = int.Parse(group_id.ToString());
            for (int i = 0; i < num; i++)
            {
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_C5_1 = num2;
                int arg_C5_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.boardDeleteTopic(arg_C5_1, arg_C5_2, num4, this.boardTopicsCount);
            }
            if (num > 0)
            {
                this.boardTopics(group_id);
            }
            this.i = 0;
            this.boardTopicsCount = 0;
            this.writeLog("Все обсуждения удалены");
            this.enabledButton(true);
        }

        public void notes()
        {
            this.enabledButton(false);
            JObject jObject = base.Send("notes.get", "count=100", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.notesCount = ((this.notesCount == 0) ? num : this.notesCount);
            num = ((num > 100) ? 100 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_AB_1 = num2;
                int num3 = this.i;
                this.i = num3 + 1;
                this.notesDelete(arg_AB_1, num3, this.notesCount);
            }
            if (num > 0)
            {
                this.notes();
            }
            this.i = 0;
            this.notesCount = 0;
            this.writeLog("Заметки удалены");
            this.enabledButton(true);
        }

        public void docs(object owner_id)
        {
            this.enabledButton(false);
            JObject jObject = base.Send("docs.get", "owner_id=" + owner_id, this.v);
            string text = (jObject["error"] ?? 0).ToString();
            if (text.IndexOf("15") >= 0)
            {
                this.writeLog("Документы в группе отключены");
            }
            else
            {
                int num = jObject["response"]["count"].Value<int>();
                for (int i = 0; i < num; i++)
                {
                    int doc_id = jObject["response"]["items"][i]["id"].Value<int>();
                    int owner_id2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                    this.docsDelete(doc_id, owner_id2, i, num);
                }
                this.writeLog("Все документы удалены");
            }
            this.enabledButton(true);
        }

        public void followers()
        {
            this.enabledButton(false);
            JObject jObject = base.Send("users.getFollowers", "count=1000", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.followersCount = ((this.followersCount == 0) ? num : this.followersCount);
            num = ((num > 1000) ? 1000 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i].Value<int>();
                int arg_A7_1 = num2;
                int num3 = this.i;
                this.i = num3 + 1;
                this.followersDelete(arg_A7_1, num3, this.followersCount);
            }
            if (num > 0)
            {
                this.followers();
            }
            this.i = 0;
            this.followersCount = 0;
            this.writeLog("Все подписчики удалены");
            this.enabledButton(true);
        }

        public void groupsMembersDeleted(object group_id)
        {
            this.enabledButton(false);
            this.writeLog("Начинаем...");
            List<int> list = new List<int>();
            int num = 0;
            JObject members = this.getMembers(group_id, ref list, num);
            int num2 = members["response"]["count"].Value<int>();
            int num3 = (int)Math.Ceiling((double)num2 / 1000.0);
            for (int i = 0; i < num3; i++)
            {
                this.getMembers(group_id, ref list, num);
                num += 1000;
            }
            int count = list.Count;
            for (int j = 0; j < count; j++)
            {
                this.groupsRemoveUser((int)group_id, list[j], j, count);
            }
            this.writeLog("Мертвые аккаунты удаленны");
            this.enabledButton(true);
        }

        private JObject getMembers(object group_id, ref List<int> deletedUsers, int offset)
        {
            Thread.Sleep(500);
            JObject jObject = base.Send("groups.getMembers", string.Concat(new object[]
            {
                    "group_id=",
                    group_id,
                    "&offset=",
                    offset,
                    "&fields=first_name"
            }), "5.44");
            this.addDeletedUsers(ref deletedUsers, jObject);
            return jObject;
        }

        private void addDeletedUsers(ref List<int> del, JObject get)
        {
            int count = JArray.Parse(get["response"]["items"].ToString()).Count;
            for (int i = 0; i < count; i++)
            {
                string a = (get["response"]["items"][i]["deactivated"] ?? "").ToString();
                if (a == "deleted")
                {
                    del.Add(get["response"]["items"][i]["id"].Value<int>());
                }
                else if (this.DelBanned && a == "banned")
                {
                    del.Add(get["response"]["items"][i]["id"].Value<int>());
                }
                this.writeLog("Найденно мертвых " + del.Count);
            }
        }

        public void groupsMembers(object group_id)
        {
            this.enabledButton(false);
            this.manager = ((this.manager.Length == 0) ? this.groupsManagers(group_id, ref this.managerCount) : this.manager);
            JObject jObject = base.Send("groups.getMembers", "group_id=" + group_id, this.v);
            int num = jObject["response"]["count"].Value<int>() - this.managerCount;
            foreach (JToken current in ((IEnumerable<JToken>)jObject["response"]["items"]))
            {
                int num2 = int.Parse(group_id.ToString());
                int num3 = current.Value<int>();
                if (this.manager.IndexOf(num3.ToString()) < 0)
                {
                    int arg_D6_1 = num2;
                    int arg_D6_2 = num3;
                    int num4 = this.i;
                    this.i = num4 + 1;
                    this.groupsRemoveUser(arg_D6_1, arg_D6_2, num4, num);
                }
            }
            if (num > 0)
            {
                this.groupsMembers(group_id);
            }
            this.i = 0;
            this.manager = "";
            this.managerCount = 0;
            this.writeLog("Подписчики удалены");
            this.enabledButton(true);
        }

        private void groupsRemoveUser(int group_id, int user_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("groups.removeUser", string.Concat(new object[]
            {
                    "group_id=",
                    group_id,
                    "&user_id=",
                    user_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено подписчиков ",
                    i,
                    " из ",
                    count
            }));
        }

        private string groupsManagers(object group_id, ref int count)
        {
            JObject jObject = base.Send("groups.getMembers", "group_id=" + group_id + "&filter=managers", this.v);
            string text = "";
            count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
            {
                text = text + jObject["response"]["items"][i]["id"].ToString() + ",";
            }
            return text.Substring(0, text.Length - 1);
        }

        private void followersDelete(int user_id, int i, int count)
        {
            Thread.Sleep(500);
            JObject jObject = base.Send("account.banUser", "user_id=" + user_id, this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удаленно подписчиков ",
                    i,
                    " из ",
                    count
            }));
        }

        private void docsDelete(int doc_id, int owner_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("docs.delete", string.Concat(new object[]
            {
                    "owner_id=",
                    owner_id,
                    "&doc_id=",
                    doc_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено документов ",
                    i,
                    " из ",
                    count
            }));
        }

        private void notesDelete(int note_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("notes.delete", "note_id=" + note_id, this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено заметок ",
                    i,
                    " из ",
                    count
            }));
        }

        private void boardDeleteTopic(int group_id, int topic_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("board.deleteTopic", string.Concat(new object[]
            {
                    "group_id=",
                    group_id,
                    "&topic_id=",
                    topic_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено обсуждений ",
                    i,
                    " из ",
                    count
            }));
        }

        private void accountUnban(int user_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("account.unbanUser", "user_id=" + user_id, this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Убранно из бана: ",
                    i,
                    " из ",
                    count
            }));
        }

        private void likesDelete(string type, int item_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("likes.delete", string.Concat(new object[]
            {
                    "type=",
                    type,
                    "&item_id=",
                    item_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено лайков ",
                    i,
                    " из ",
                    count
            }));
        }

        private void newsfeedDelete(int list_id, int i, int count)
        {
            Thread.Sleep(500);
            JObject jObject = base.Send("newsfeed.deleteList", "list_id=" + list_id, this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено новостных списков ",
                    i,
                    " из ",
                    count
            }));
        }

        private int messageHistory(int user_id)
        {
            Thread.Sleep(1000);
            JObject jObject = base.Send("messages.getHistory", "user_id=" + user_id, this.v);
            return jObject["response"]["count"].Value<int>();
        }

        private void messagesDelete(int user_id, int chat_id, int i, int count)
        {
            Thread.Sleep(500);
            int num = (chat_id > 0) ? chat_id : user_id;
            if (chat_id > 0)
            {
                base.Send("messages.deleteDialog", "chat_id=" + num, this.v);
            }
            else
            {
                base.Send("messages.deleteDialog", "user_id=" + num, this.v);
            }
            if (user_id > 0 && this.messageHistory(user_id) > 10000 && chat_id < 0)
            {
                this.messagesDelete(user_id, chat_id, i, count);
            }
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено диалогов ",
                    i,
                    " из ",
                    count
            }));
        }

        private void audioDelete(int owner_id, int audio_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("audio.delete", string.Concat(new object[]
            {
                    "audio_id=",
                    audio_id,
                    "&owner_id=",
                    owner_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено аудиозаписей ",
                    i,
                    " из ",
                    count
            }));
        }

        private void videoAlbum(object owner_id)
        {
            Thread.Sleep(500);
            JObject jObject = base.Send("video.getAlbums", "owner_id=" + owner_id + "&count=100", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.videoCount = ((this.videoCount == 0) ? num : this.videoCount);
            num = ((num > 100) ? 100 : num);
            for (int i = 0; i < num; i++)
            {
                int owner_id2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int album_id = jObject["response"]["items"][i]["id"].Value<int>();
                this.videoAlbumDelete(owner_id2, album_id, this.i, this.videoCount);
            }
            if (num > 0)
            {
                this.videoAlbum(owner_id);
            }
            this.i = 0;
            this.videoCount = 0;
            this.writeLog("Видео и видеоальбомы удалены");
        }

        private void videoAlbumDelete(int owner_id, int album_id, int i, int count)
        {
            int num = (owner_id < 0) ? Math.Abs(owner_id) : 0;
            base.Send("video.deleteAlbum", string.Concat(new object[]
            {
                    "group_id=",
                    num,
                    "&album_id=",
                    album_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено видеоальбомов ",
                    i,
                    " из ",
                    count
            }));
        }

        private void videoDelete(int video_id, int owner_id, int target_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("video.delete", string.Concat(new object[]
            {
                    "video_id=",
                    video_id,
                    "&owner_id=",
                    owner_id,
                    "&target_id=",
                    target_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено видео ",
                    i,
                    " из ",
                    count
            }));
        }

        private void photosSaved()
        {
            JObject jObject = base.Send("photos.get", "album_id=saved", this.v);
            int num = jObject["response"]["count"].Value<int>();
            this.photoCount = ((this.photoCount == 0) ? num : this.photoCount);
            num = ((num >= 1000) ? 1000 : num);
            for (int i = 0; i < num; i++)
            {
                int num2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int num3 = jObject["response"]["items"][i]["id"].Value<int>();
                int arg_E5_1 = num2;
                int arg_E5_2 = num3;
                int num4 = this.i;
                this.i = num4 + 1;
                this.photoDelete(arg_E5_1, arg_E5_2, num4, this.photoCount, "Удалено сохраненых фотографий ");
            }
            if (num > 0)
            {
                this.photosSaved();
            }
            this.writeLog("Сохраненые фотографии удалены");
            this.i = 0;
            this.photoCount = 0;
        }

        private void photosAlbumDelete(int owner_id, int album_id, int i, int count)
        {
            Thread.Sleep(500);
            int num = (owner_id < 0) ? owner_id : 0;
            base.Send("photos.deleteAlbum", string.Concat(new object[]
            {
                    "album_id=",
                    album_id,
                    "&group_id=",
                    num
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удаленно альбомов ",
                    i,
                    " из ",
                    count
            }));
        }

        private void photoDelete(int owner_id, int photo_id, int i, int count, string text = "Удалено фотографий ")
        {
            Thread.Sleep(1000);
            base.Send("photos.delete", string.Concat(new object[]
            {
                    "owner_id=",
                    owner_id,
                    "&photo_id=",
                    photo_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    text,
                    i,
                    " из ",
                    count
            }));
        }

        private void friendsDelete(int user_id, int i, int count)
        {
            Thread.Sleep(1000);
            base.Send("friends.delete", "user_id=" + user_id, this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удаленно друзей ",
                    i,
                    " из ",
                    count
            }));
        }

        private void groupsUnbanUser(int group_id, int user_id, int i, int count)
        {
            Thread.Sleep(500);
            base.Send("groups.unbanUser", string.Concat(new object[]
            {
                    "group_id=",
                    group_id,
                    "&user_id=",
                    user_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Убрано из бана ",
                    i,
                    " из ",
                    count
            }));
        }

        private void leaveGroups(int group_id, int i, int count)
        {
            Thread.Sleep(1000);
            base.Send("groups.leave", "group_id=" + group_id, this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Покинуто ",
                    i,
                    " из ",
                    count
            }));
        }

        private void wallDelete(int owner_id, int post_id, int i = 0, int count = 0)
        {
            Thread.Sleep(500);
            base.Send("wall.delete", string.Concat(new object[]
            {
                    "owner_id=",
                    owner_id,
                    "&post_id=",
                    post_id
            }), this.v);
            this.writeLog(string.Concat(new object[]
            {
                    "Удалено записей ",
                    i,
                    " из ",
                    count
            }));
        }

        private void writeLog(string text)
        {
            this.form.setTextLabel(text);
        }

        private void enabledButton(bool btnEnabled)
        {
            this.form.setButtonEnabled(btnEnabled);
        }
    }
}
