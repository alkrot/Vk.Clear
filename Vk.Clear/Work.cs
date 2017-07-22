using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;
using VkApi;

namespace Vk.Clear
{
    public class Work : ApiVk
    {
        private readonly string _v;

        private Form1 _form;

        private int _wallCount;

        private int _photoCount;

        private int _videoCount;

        private int _messagesCount;

        private int _bannedCount;

        private int _boardTopicsCount;

        private int _notesCount;

        private int _followersCount;

        private string _manager = "";

        private int _managerCount;

        private int _i;

        public bool DelSavePhoto { get; set; }

        public bool DelAlbumVideo { get; set; }

        public bool DelBanned { get; set; }

        public bool DelWallPhotoGroup { get; set; }

        public Work(string v = "5.44")
        {
            _v = v;
        }

        public void StatsTrackVisitor()
        {
            Send("stats.trackVisitor", "", _v);
        }

        public void AddForms(Form1 form)
        {
            _form = form;
        }

        public void Groups()
        {
            EnabledButton(false);
            JObject jObject = Send("groups.get", "", _v);
            int num = jObject["response"]["count"].Value<int>();
            foreach (JToken current in jObject["response"]["items"])
            {
                int groupId = current.Value<int>();
                LeaveGroups(groupId, _i++, num);
            }
            if (num > 0)
                Groups();
            EnabledButton(true);
            WriteLog("Вышли");
            _i = 0;
        }

        public void GroupsBanned(object groupId)
        {
            EnabledButton(false);
            int groupId2 = Math.Abs(int.Parse(groupId.ToString()));
            JObject jObject = Send("groups.getBanned", "group_id=" + groupId2 + "&count=200", _v);
            int count = jObject["response"]["count"].Value<int>();
            _bannedCount = _bannedCount == 0 ? count : _bannedCount;
            count = count > 200 ? 200 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = jObject["response"]["items"][i]["id"].Value<int>();
                GroupsUnbanUser(groupId2, userId, _i, _bannedCount);
            }
            if (count > 0)
                GroupsBanned(groupId);
            _i = 0;
            _bannedCount = 0;
            WriteLog("Пользователи в группе разбаненны");
            EnabledButton(true);
        }

        public void Friends()
        {
            EnabledButton(false);
            JObject jObject = Send("friends.get", "", _v);
            int count = jObject["response"]["count"].Value<int>();
            foreach (JToken current in jObject["response"]["items"])
            {
                int userId = current.Value<int>();
                FriendsDelete(userId, _i++, count);
            }
            WriteLog("У Вас, больше нет друзей");
            _i = 0;
            EnabledButton(true);
        }

        public void Wall(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("wall.get", "owner_id=" + ownerId + "&count=100", _v);
            int count = jObject["response"]["count"].Value<int>();
            _wallCount = _wallCount == 0 ? count : _wallCount;
            count = count >= 100 ? 100 : count;
            for (int i = 0; i < count; i++)
            {
                int postId = jObject["response"]["items"][i]["id"].Value<int>();
                int ownwerId = jObject["response"]["items"][i]["owner_id"].Value<int>();
                WallDelete(ownwerId, postId, _i, _wallCount);
            }
            if (count > 0)
                Wall(ownerId);
            _wallCount = 0;
            WriteLog("Чистка стены, закончена");
            _i = 0;
            EnabledButton(true);
        }

        public void Photos(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("photos.getAll", "owner_id=" + ownerId + "&count=200", _v);
            int num = jObject["response"]["count"].Value<int>();
            _photoCount = _photoCount == 0 ? num : _photoCount;
            num = num >= 200 ? 200 : num;
            for (int i = 0; i < num; i++)
            {
                int ownerIdp = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int photoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotoDelete(ownerIdp, photoId, _i, _photoCount);
            }
            if (num > 0)
                Photos(ownerId);
            WriteLog("Фотографии удаленны");
            _i = 0;
            _photoCount = 0;
            if (DelSavePhoto && ownerId.ToString() == ID.ToString())
            {
                WriteLog("Начинаем парсить...");
                PhotosSaved();
            }
            else if (DelWallPhotoGroup)
            {
                WriteLog("Начинаем парсить...");
                PhotosWall(ownerId.ToString());
            }
            EnabledButton(true);
        }

        private void PhotosWall(string ownerId)
        {
            Thread.Sleep(1000);
            JObject jObject = Send("photos.get", "owner_id=" + ownerId + "&album_id=wall&count=200&rev=1");
            int num = jObject["response"]["count"].Value<int>();
            _photoCount = _photoCount == 0 ? num : _photoCount;
            num = num > 200 ? 200 : num;
            for (int i = 1; i < num; i++)
            {
                int ownerIdPw = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int photoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotoDelete(ownerIdPw, photoId, _i, _photoCount);
            }
            if (num > 0 && _i < 1000)
                PhotosWall(ownerId);
            _i = 0;
            _photoCount = 0;
            WriteLog("Удалили (максимум 1000) за раз");
        }

        public void PhotosAlbum(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("photos.getAlbums", "owner_id=" + ownerId, _v);
            int count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
            {
                int ownerIdPa = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int photoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotosAlbumDelete(ownerIdPa, photoId, _i, count);
            }
            WriteLog("Альбомы удалены");
            _i = 0;
            EnabledButton(true);
        }

        public void Video(object ownerId)
        {
            EnabledButton(false);
            WriteLog("Получаем видео");
            Thread.Sleep(500);
            JObject jObject = Send("video.get", "owner_id=" + ownerId + "&count=200", _v);
            JArray jArray = jObject["response"]["items"].Value<JArray>();
            int count = jObject["response"]["count"].Value<int>();
            _videoCount = _videoCount == 0 ? count : _videoCount;
            for (int i = 0; i < jArray.Count; i++)
            {
                int ownerIdVideo = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int videoId = jObject["response"]["items"][i]["id"].Value<int>();
                int targetId = int.Parse(ownerId.ToString());
                VideoDelete(videoId, ownerIdVideo, targetId, _i, _videoCount);
            }
            if (count > 0)
                Video(ownerId);
            WriteLog("Видео удаленны");
            _videoCount = 0;
            _i = 0;
            if (DelAlbumVideo)
                VideoAlbum(ownerId);
            EnabledButton(true);
        }

        public void Audio(object ownerId)
        {
            EnabledButton(false);
            WriteLog("Удаление аудиозаписей не работает");
            EnabledButton(true);
        }

        public void Messages()
        {
            EnabledButton(false);
            JObject jObject = Send("messages.getDialogs", "count=200", _v);
            int count = jObject["response"]["count"].Value<int>();
            _messagesCount = _messagesCount == 0 ? count : _messagesCount;
            count = count > 200 ? 200 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = (jObject["response"]["items"][i]["message"]["user_id"] ?? -1).Value<int>();
                int chatId = (jObject["response"]["items"][i]["message"]["chat_id"] ?? -1).Value<int>();
                MessagesDelete(userId, chatId, _i, _messagesCount);
            }
            if (count > 0)
                Messages();
            _i = 0;
            _messagesCount = 0;
            WriteLog("Все диалоги удалены");
            EnabledButton(true);
        }

        public void NewsfeedLists()
        {
            EnabledButton(false);
            JObject jObject = Send("newsfeed.getLists", "", _v);
            int count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
            {
                int listId = jObject["response"]["items"][i]["id"].Value<int>();
                NewsfeedDelete(listId, i, count);
            }
            WriteLog("Все списки удалены");
            EnabledButton(true);
        }

        public void AccountBanned()
        {
            EnabledButton(false);
            JObject jObject = Send("account.getBanned", "count=200", _v);
            int count = jObject["response"]["count"].Value<int>();
            _bannedCount = _bannedCount == 0 ? count : _bannedCount;
            count = count > 200 ? 200 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = jObject["response"]["items"][i]["id"].Value<int>();
                AccountUnban(userId, _i, _bannedCount);
            }
            if (count > 0)
                AccountBanned();
            _i = 0;
            _bannedCount = 0;
            WriteLog("Ваш черный список чист");
            EnabledButton(true);
        }

        public void BoardTopics(object groupId)
        {
            EnabledButton(false);
            JObject jObject = Send("board.getTopics", "group_id=" + groupId + "&count=100", _v);
            int count = jObject["response"]["count"].Value<int>();
            _boardTopicsCount = _boardTopicsCount == 0 ? count : _boardTopicsCount;
            count = count > 100 ? 100 : count;
            int groupIdInt = int.Parse(groupId.ToString());
            for (int i = 0; i < count; i++)
            {
                int topicId = jObject["response"]["items"][i]["id"].Value<int>();
                BoardDeleteTopic(groupIdInt, topicId, _i, _boardTopicsCount);
            }
            if (count > 0)
                BoardTopics(groupId);
            _i = 0;
            _boardTopicsCount = 0;
            WriteLog("Все обсуждения удалены");
            EnabledButton(true);
        }

        public void Notes()
        {
            EnabledButton(false);
            JObject jObject = Send("notes.get", "count=100", _v);
            int count = jObject["response"]["count"].Value<int>();
            _notesCount = _notesCount == 0 ? count : _notesCount;
            count = count > 100 ? 100 : count;
            for (int i = 0; i < count; i++)
            {
                int noteId = jObject["response"]["items"][i]["id"].Value<int>();
                NotesDelete(noteId, _i, _notesCount);
            }
            if (count > 0)
                Notes();
            _i = 0;
            _notesCount = 0;
            WriteLog("Заметки удалены");
            EnabledButton(true);
        }

        public void Docs(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("docs.get", "owner_id=" + ownerId, _v);
            string text = (jObject["error"] ?? 0).ToString();
            if ("15".IndexOf(text, StringComparison.Ordinal) >= 0)
                WriteLog("Документы в группе отключены");
            else
            {
                int count = jObject["response"]["count"].Value<int>();
                for (int i = 0; i < count; i++)
                {
                    int docId = jObject["response"]["items"][i]["id"].Value<int>();
                    int ownerId2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                    DocsDelete(docId, ownerId2, i, count);
                }
                WriteLog("Все документы удалены");
            }
            EnabledButton(true);
        }

        public void Followers()
        {
            EnabledButton(false);
            JObject jObject = Send("users.getFollowers", "count=1000", _v);
            int count = jObject["response"]["count"].Value<int>();
            _followersCount = _followersCount == 0 ? count : _followersCount;
            count = count > 1000 ? 1000 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = jObject["response"]["items"][i].Value<int>();
                FollowersDelete(userId, _i, _followersCount);
            }
            if (count > 0)
                Followers();
            _i = 0;
            _followersCount = 0;
            WriteLog("Все подписчики удалены");
            EnabledButton(true);
        }

        public void GroupsMembersDeleted(object groupId)
        {
            EnabledButton(false);
            WriteLog("Начинаем...");
            List<int> list = new List<int>();
            int offset = 0;
            JObject members = GetMembers(groupId, ref list, offset);
            int countMembers = members["response"]["count"].Value<int>();
            int repeat = (int)Math.Ceiling(countMembers / 1000.0);
            for (int i = 0; i < repeat; i++)
            {
                GetMembers(groupId, ref list, offset);
                offset += 1000;
            }
            int count = list.Count;
            for (int j = 0; j < count; j++)
                GroupsRemoveUser((int) groupId, list[j], j, count);
            WriteLog("Мертвые аккаунты удаленны");
            EnabledButton(true);
        }

        private JObject GetMembers(object groupId, ref List<int> deletedUsers, int offset)
        {
            Thread.Sleep(500);
            JObject jObject = Send("groups.getMembers", param: string.Concat("group_id=", groupId, "&offset=", offset, "&fields=first_name"));
            AddDeletedUsers(ref deletedUsers, jObject);
            return jObject;
        }

        private void AddDeletedUsers(ref List<int> del, JObject get)
        {
            int count = JArray.Parse(get["response"]["items"].ToString()).Count;
            for (int i = 0; i < count; i++)
            {
                string deactivated = (get["response"]["items"][i]["deactivated"] ?? "").ToString();
                if (deactivated == "deleted")
                {
                    del.Add(get["response"]["items"][i]["id"].Value<int>());
                }
                else if (DelBanned && deactivated == "banned")
                {
                    del.Add(get["response"]["items"][i]["id"].Value<int>());
                }
                WriteLog("Найденно мертвых " + del.Count);
            }
        }

        public void GroupsMembers(object groupId)
        {
            EnabledButton(false);
            _manager = _manager.Length == 0 ? GroupsManagers(groupId, ref _managerCount) : _manager;
            JObject jObject = Send("groups.getMembers", "group_id=" + groupId, _v);
            int count = jObject["response"]["count"].Value<int>() - _managerCount;
            foreach (JToken current in jObject["response"]["items"])
            {
                int groupId2 = int.Parse(groupId.ToString());
                int userId = current.Value<int>();
                if (_manager.IndexOf(userId.ToString(), StringComparison.Ordinal) < 0)
                    GroupsRemoveUser(groupId2, userId, _i++, count);
            }
            if (count > 0)
                GroupsMembers(groupId);
            _i = 0;
            _manager = "";
            _managerCount = 0;
            WriteLog("Подписчики удалены");
            EnabledButton(true);
        }

        private void GroupsRemoveUser(int groupId, int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("groups.removeUser", string.Concat("group_id=", groupId, "&user_id=", userId), _v);
            WriteLog(string.Concat("Удалено подписчиков ", i, " из ", count));
        }

        private string GroupsManagers(object groupId, ref int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            JObject jObject = Send("groups.getMembers", "group_id=" + groupId + "&filter=managers", _v);
            string text = "";
            count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
                text = text + jObject["response"]["items"][i]["id"] + ",";
            return text.Substring(0, text.Length - 1);
        }

        private void FollowersDelete(int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("account.banUser", "user_id=" + userId, _v);
            WriteLog(string.Concat("Удаленно подписчиков ", i, " из ", count));
        }

        private void DocsDelete(int docId, int ownerId, int i, int count)
        {
            Thread.Sleep(500);
            Send("docs.delete", string.Concat("owner_id=", ownerId, "&doc_id=", docId), _v);
            WriteLog(string.Concat("Удалено документов ", i, " из ", count));
        }

        private void NotesDelete(int noteId, int i, int count)
        {
            Thread.Sleep(500);
            Send("notes.delete", "note_id=" + noteId, _v);
            WriteLog(string.Concat("Удалено заметок ", i, " из ", count));
        }

        private void BoardDeleteTopic(int groupId, int topicId, int i, int count)
        {
            Thread.Sleep(500);
            Send("board.deleteTopic", string.Concat("group_id=", groupId, "&topic_id=", topicId), _v);
            WriteLog(string.Concat("Удалено обсуждений ", i, " из ", count));
        }

        private void AccountUnban(int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("account.unbanUser", "user_id=" + userId, _v);
            WriteLog(string.Concat("Убранно из бана: ", i, " из ", count));
        }

        /*
        private void LikesDelete(string type, int itemId, int i, int count)
        {
            Thread.Sleep(500);
            Send("likes.delete", string.Concat("type=", type, "&item_id=", itemId), _v);
            WriteLog(string.Concat("Удалено лайков ", i, " из ", count));
        }*/

        private void NewsfeedDelete(int listId, int i, int count)
        {
            Thread.Sleep(500);
            Send("newsfeed.deleteList", "list_id=" + listId, _v);
            WriteLog(string.Concat("Удалено новостных списков ", i, " из ", count));
        }

        private int MessageHistory(int userId)
        {
            Thread.Sleep(1000);
            JObject jObject = Send("messages.getHistory", "user_id=" + userId, _v);
            return jObject["response"]["count"].Value<int>();
        }

        private void MessagesDelete(int userId, int chatId, int i, int count)
        {
            Thread.Sleep(500);
            int dialogId = chatId > 0 ? chatId : userId;
            if (chatId > 0)
                Send("messages.deleteDialog", "chat_id=" + dialogId, _v);
            else
                Send("messages.deleteDialog", "user_id=" + dialogId, _v);
            if (userId > 0 && MessageHistory(userId) > 10000 && chatId < 0)
                MessagesDelete(userId, chatId, i, count);
            WriteLog(string.Concat("Удалено диалогов ", i, " из ", count));
        }

        /*
        private void AudioDelete(int ownerId, int audioId, int i, int count)
        {
            Thread.Sleep(500);
            Send("audio.delete", string.Concat("audio_id=", audioId, "&owner_id=", ownerId), _v);
            WriteLog(string.Concat("Удалено аудиозаписей ", i, " из ", count));
        }*/

        private void VideoAlbum(object ownerId)
        {
            Thread.Sleep(500);
            JObject jObject = Send("video.getAlbums", "owner_id=" + ownerId + "&count=100", _v);
            int count = jObject["response"]["count"].Value<int>();
            _videoCount = _videoCount == 0 ? count : _videoCount;
            count = count > 100 ? 100 : count;
            for (int i = 0; i < count; i++)
            {
                int ownerId2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int albumId = jObject["response"]["items"][i]["id"].Value<int>();
                VideoAlbumDelete(ownerId2, albumId, _i, _videoCount);
            }
            if (count > 0)
                VideoAlbum(ownerId);
            _i = 0;
            _videoCount = 0;
            WriteLog("Видео и видеоальбомы удалены");
        }

        private void VideoAlbumDelete(int ownerId, int albumId, int i, int count)
        {
            int num = ownerId < 0 ? Math.Abs(ownerId) : 0;
            Send("video.deleteAlbum", string.Concat("group_id=", num, "&album_id=", albumId), _v);
            WriteLog(string.Concat("Удалено видеоальбомов ", i, " из ", count));
        }

        private void VideoDelete(int videoId, int ownerId, int targetId, int i, int count)
        {
            Thread.Sleep(500);
            Send("video.delete", string.Concat("video_id=", videoId, "&owner_id=", ownerId, "&target_id=", targetId), _v);
            WriteLog(string.Concat("Удалено видео ", i, " из ", count));
        }

        private void PhotosSaved()
        {
            JObject jObject = Send("photos.get", "album_id=saved", _v);
            int count = jObject["response"]["count"].Value<int>();
            _photoCount = _photoCount == 0 ? count : _photoCount;
            count = count >= 1000 ? 1000 : count;
            for (int i = 0; i < count; i++)
            {
                int ownerId2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int phtoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotoDelete(ownerId2, phtoId, _i, _photoCount, "Удалено сохраненых фотографий ");
            }
            if (count > 0)
                PhotosSaved();
            WriteLog("Сохраненые фотографии удалены");
            _i = 0;
            _photoCount = 0;
        }

        private void PhotosAlbumDelete(int ownerId, int albumId, int i, int count)
        {
            Thread.Sleep(500);
            int num = ownerId < 0 ? ownerId : 0;
            Send("photos.deleteAlbum", string.Concat("album_id=", albumId, "&group_id=", num), _v);
            WriteLog(string.Concat("Удаленно альбомов ", i, " из ", count));
        }

        private void PhotoDelete(int ownerId, int photoId, int i, int count, string text = "Удалено фотографий ")
        {
            Thread.Sleep(1000);
            Send("photos.delete", string.Concat("owner_id=", ownerId, "&photo_id=", photoId), _v);
            WriteLog(string.Concat(text, i, " из ", count));
        }

        private void FriendsDelete(int userId, int i, int count)
        {
            Thread.Sleep(1000);
            Send("friends.delete", "user_id=" + userId, _v);
            WriteLog(string.Concat("Удаленно друзей ", i, " из ", count));
        }

        private void GroupsUnbanUser(int groupId, int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("groups.unbanUser", string.Concat("group_id=", groupId, "&user_id=", userId), _v);
            WriteLog(string.Concat("Убрано из бана ", i, " из ", count));
        }

        private void LeaveGroups(int groupId, int i, int count)
        {
            Thread.Sleep(1000);
            Send("groups.leave", "group_id=" + groupId, _v);
            WriteLog(string.Concat("Покинуто ", i, " из ", count));
        }

        private void WallDelete(int ownerId, int postId, int i = 0, int count = 0)
        {
            Thread.Sleep(500);
            Send("wall.delete", string.Concat("owner_id=", ownerId, "&post_id=", postId), _v);
            WriteLog(string.Concat("Удалено записей ", i, " из ", count));
        }

        private void WriteLog(string text)
        {
            _form.SetTextLabel(text);
        }

        private void EnabledButton(bool btnEnabled)
        {
            _form.SetButtonEnabled(btnEnabled);
        }
    }
}
