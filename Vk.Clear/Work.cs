using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;
using VkApi;

namespace Vk.Clear
{
    /// <summary>
    /// Class for work with vk api
    /// </summary>
    public partial class Work : ApiVk
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="v">Version vk api</param>
        public Work(string v = "5.68")
        {
            this.v = v;
        }

        /// <summary>
        /// send visited user
        /// </summary>
        public void StatsTrackVisitor()
        {
            Send("stats.trackVisitor", "", v);
        }
        /// <summary>
        /// Forms uses
        /// </summary>
        /// <param name="form">form</param>
        public void AddForms(Form1 form)
        {
            this.form = form;
        }

        /// <summary>
        /// Delete photo of fave
        /// </summary>
        public void FavePhotos()
        {
            EnabledButton(false);
            JObject jObject = Send("fave.getPhotos", "", v);
            int count = jObject["response"]["count"].Value<int>();
            jObject = Send("fave.getPhotos", "count=" + count, v);
            foreach (var photo in jObject["response"]["items"])
            {
                int owner_id = photo["owner_id"].Value<int>();
                int item_id = photo["id"].Value<int>();
                if (LikesDelete("photo", owner_id, item_id) >= 0)
                    WriteLog("Удаленно фото из закладок " + ++i + "/" + count);
                Thread.Sleep(1000);
            }
            i = 0;
            EnabledButton(true);
            WriteLog("Удаленны фото из закладок");
        }

        /// <summary>
        /// Delete video of fave
        /// </summary>
        public void FaveVideo()
        {
            EnabledButton(false);
            JObject jObject = Send("fave.getVideos", "", v);
            int count = jObject["response"]["count"].Value<int>();
            jObject = Send("fave.getVideos", "count=" + count, v);
            foreach (var photo in jObject["response"]["items"])
            {
                int owner_id = photo["owner_id"].Value<int>();
                int item_id = photo["id"].Value<int>();
                if (LikesDelete("video", owner_id, item_id) >= 0)
                    WriteLog("Удаленно видео из закладок " + ++i + "/" + count);
                Thread.Sleep(1000);
            }
            i = 0;
            EnabledButton(true);
            WriteLog("Удаленны фото из закладок");
        }

        /// <summary>
        /// Delete post of fave
        /// </summary>
        /// <param name="w_count">Count element</param>
        public void FavePost(object w_count)
        {
            int wcount = int.Parse(w_count.ToString());
            try
            {
                EnabledButton(false);
                JObject jObject = Send("fave.getPosts", "count=" + wcount, v);
                int count = jObject["response"]["count"].Value<int>();
                wallCount = wallCount == 0 ? count : wallCount;
                count = count > 100 ? 100 : count;
                foreach (JToken post in jObject["response"]["items"])
                {
                    if (LikesDelete(post["post_type"].ToString(), post["owner_id"].Value<int>(), post["id"].Value<int>()) >= 0)
                    {
                        WriteLog("Удаленно постов из закладок " + ++i + "/" + wallCount);
                    }
                    Thread.Sleep(1000);
                }
                if (count > 0) FavePost(wcount);
            }
            catch (NullReferenceException er)
            {
                FavePost(wcount + 500);
                WriteLog(er.Message);
            }
            finally
            {
                wallCount = 0;
                i = 0;
                WriteLog("Посты из закладок удалены");
                EnabledButton(true);
            }
        }

        /// <summary>
        /// Delete user of fave
        /// </summary>
        public void FaveUsers()
        {
            EnabledButton(false);
            JObject jObject = Send("fave.getUsers", "");
            int count = jObject["response"]["count"].Value<int>();
            if (count > 50) jObject = Send("fave.getUsers", "count=" + count);
            foreach (JToken users in jObject["response"]["items"])
            {
                if (RemoveFaveUser(users["id"].Value<int>()) > 0)
                {
                    WriteLog("Удаленно пользователей из зкаладок " + ++i + "/" + count);
                    Thread.Sleep(1000);
                }
            }
            i = 0;
            WriteLog("Пользователи из закладок удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete link of fave
        /// </summary>
        public void FaveLink()
        {
            EnabledButton(false);
            JObject jObject = Send("fave.getLinks", "", v);
            int count = jObject["response"]["count"].Value<int>();
            if (count > 50) jObject = Send("fave.getLinks", "count=" + count, v);
            foreach (JToken link in jObject["response"]["items"])
            {
                if (RemoveLink(link["id"].ToString()) > 0)
                {
                    WriteLog("Удалено ссылок из закладок " + ++i + "/" + count);
                    Thread.Sleep(1000);
                }
            }
            i = 0;
            WriteLog("Все ссылки удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Out of group
        /// </summary>
        public void Groups()
        {
            EnabledButton(false);
            JObject jObject = Send("groups.get", "", v);
            int num = jObject["response"]["count"].Value<int>();
            foreach (JToken current in jObject["response"]["items"])
            {
                int groupId = current.Value<int>();
                LeaveGroups(groupId, i++, num);
            }
            if (num > 0)
                Groups();
            EnabledButton(true);
            WriteLog("Вышли");
            i = 0;
        }

        /// <summary>
        /// Clear blacklist in group
        /// </summary>
        /// <param name="groupId">Group id</param>
        public void GroupsBanned(object groupId)
        {
            EnabledButton(false);
            int groupId2 = Math.Abs(int.Parse(groupId.ToString()));
            JObject jObject = Send("groups.getBanned", "group_id=" + groupId2 + "&count=200", v);
            int count = jObject["response"]["count"].Value<int>();
            bannedCount = bannedCount == 0 ? count : bannedCount;
            count = count > 200 ? 200 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = jObject["response"]["items"][i]["id"].Value<int>();
                GroupsUnbanUser(groupId2, userId, ++this.i, bannedCount);
            }
            if (count > 0)
                GroupsBanned(groupId);
            i = 0;
            bannedCount = 0;
            WriteLog("Пользователи в группе разбаненны");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete friends
        /// </summary>
        public void Friends()
        {
            EnabledButton(false);
            JObject jObject = Send("friends.get", "", v);
            int count = jObject["response"]["count"].Value<int>();
            foreach (JToken current in jObject["response"]["items"])
            {
                int userId = current.Value<int>();
                FriendsDelete(userId, i++, count);
            }
            WriteLog("У Вас, больше нет друзей");
            i = 0;
            EnabledButton(true);
        }

        /// <summary>
        /// Delete post on wall
        /// </summary>
        /// <param name="ownerId">User id or group id</param>
        public void Wall(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("wall.get", "owner_id=" + ownerId + "&count=100", v);
            int count = jObject["response"]["count"].Value<int>();
            wallCount = wallCount == 0 ? count : wallCount;
            count = count >= 100 ? 100 : count;
            for (int i = 0; i < count; i++)
            {
                int postId = jObject["response"]["items"][i]["id"].Value<int>();
                int ownwerId = jObject["response"]["items"][i]["owner_id"].Value<int>();
                WallDelete(ownwerId, postId, ++this.i, wallCount);
            }
            if (count > 0)
                Wall(ownerId);
            wallCount = 0;
            WriteLog("Чистка стены, закончена");
            i = 0;
            EnabledButton(true);
        }

        /// <summary>
        /// Delete photo in group or user
        /// </summary>
        /// <param name="ownerId">Group id or user id</param>
        public void Photos(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("photos.getAll", "owner_id=" + ownerId + "&count=200", v);
            int num = jObject["response"]["count"].Value<int>();
            photoCount = photoCount == 0 ? num : photoCount;
            num = num >= 200 ? 200 : num;
            for (int i = 0; i < num; i++)
            {
                int ownerIdp = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int photoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotoDelete(ownerIdp, photoId, ++this.i, photoCount);
            }
            if (num > 0)
                Photos(ownerId);
            WriteLog("Фотографии удаленны");
            i = 0;
            photoCount = 0;
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

        /// <summary>
        /// Delete albums
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        public void PhotosAlbum(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("photos.getAlbums", "owner_id=" + ownerId, v);
            int count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
            {
                int ownerIdPa = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int photoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotosAlbumDelete(ownerIdPa, photoId, ++this.i, count);
            }
            WriteLog("Альбомы удалены");
            i = 0;
            EnabledButton(true);
        }

        /// <summary>
        /// Delete videos
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        public void Video(object ownerId)
        {
            EnabledButton(false);
            WriteLog("Получаем видео");
            Thread.Sleep(500);
            JObject jObject = Send("video.get", "owner_id=" + ownerId + "&count=200", v);
            JArray jArray = jObject["response"]["items"].Value<JArray>();
            int count = jObject["response"]["count"].Value<int>();
            videoCount = videoCount == 0 ? count : videoCount;
            for (int i = 0; i < jArray.Count; i++)
            {
                int ownerIdVideo = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int videoId = jObject["response"]["items"][i]["id"].Value<int>();
                int targetId = int.Parse(ownerId.ToString());
                VideoDelete(videoId, ownerIdVideo, targetId, ++this.i, videoCount);
            }
            if (count > 0)
                Video(ownerId);
            WriteLog("Видео удаленны");
            videoCount = 0;
            i = 0;
            if (DelAlbumVideo)
                VideoAlbum(ownerId);
            EnabledButton(true);
        }

        /// <summary>
        /// Delete audios
        /// </summary>
        /// <param name="ownerId">User or group id</param>
        /// Here not work method, because app not officall and not get audio
        public void Audio(object ownerId)
        {
            EnabledButton(false);
            WriteLog("Удаление аудиозаписей не работает");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete messages
        /// </summary>
        public void Messages()
        {
            EnabledButton(false);
            JObject jObject = Send("messages.getDialogs", "count=200", v);
            int count = jObject["response"]["count"].Value<int>();
            messagesCount = messagesCount == 0 ? count : messagesCount;
            count = count > 200 ? 200 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = (jObject["response"]["items"][i]["message"]["user_id"] ?? -1).Value<int>();
                int chatId = (jObject["response"]["items"][i]["message"]["chat_id"] ?? -1).Value<int>();
                MessagesDelete(userId, chatId, ++this.i, messagesCount);
            }
            if (count > 0)
                Messages();
            i = 0;
            messagesCount = 0;
            WriteLog("Все диалоги удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete news list's
        /// </summary>
        public void NewsfeedLists()
        {
            EnabledButton(false);
            JObject jObject = Send("newsfeed.getLists", "", v);
            int count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
            {
                int listId = jObject["response"]["items"][i]["id"].Value<int>();
                NewsfeedDelete(listId, i, count);
            }
            WriteLog("Все списки удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Clear blacklist current user
        /// </summary>
        public void AccountBanned()
        {
            EnabledButton(false);
            JObject jObject = Send("account.getBanned", "count=200", v);
            int count = jObject["response"]["count"].Value<int>();
            bannedCount = bannedCount == 0 ? count : bannedCount;
            count = count > 200 ? 200 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = jObject["response"]["items"][i]["id"].Value<int>();
                AccountUnban(userId, ++this.i, bannedCount);
            }
            if (count > 0)
                AccountBanned();
            i = 0;
            bannedCount = 0;
            WriteLog("Ваш черный список чист");
            EnabledButton(true);
        }

        /// <summary>
        /// In group delete topic's
        /// </summary>
        /// <param name="groupId">Group id</param>
        public void BoardTopics(object groupId)
        {
            EnabledButton(false);
            JObject jObject = Send("board.getTopics", "group_id=" + groupId + "&count=100", v);
            int count = jObject["response"]["count"].Value<int>();
            boardTopicsCount = boardTopicsCount == 0 ? count : boardTopicsCount;
            count = count > 100 ? 100 : count;
            int groupIdInt = int.Parse(groupId.ToString());
            for (int i = 0; i < count; i++)
            {
                int topicId = jObject["response"]["items"][i]["id"].Value<int>();
                BoardDeleteTopic(groupIdInt, topicId, ++this.i, boardTopicsCount);
            }
            if (count > 0)
                BoardTopics(groupId);
            i = 0;
            boardTopicsCount = 0;
            WriteLog("Все обсуждения удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete notes
        /// </summary>
        public void Notes()
        {
            EnabledButton(false);
            JObject jObject = Send("notes.get", "count=100", v);
            int count = jObject["response"]["count"].Value<int>();
            notesCount = notesCount == 0 ? count : notesCount;
            count = count > 100 ? 100 : count;
            for (int i = 0; i < count; i++)
            {
                int noteId = jObject["response"]["items"][i]["id"].Value<int>();
                NotesDelete(noteId, ++this.i, notesCount);
            }
            if (count > 0)
                Notes();
            i = 0;
            notesCount = 0;
            WriteLog("Заметки удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete doc
        /// </summary>
        /// <param name="ownerId">Group id or user id</param>
        public void Docs(object ownerId)
        {
            EnabledButton(false);
            JObject jObject = Send("docs.get", "owner_id=" + ownerId, v);
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

        /// <summary>
        /// Delete (banned) followers user
        /// </summary>
        public void Followers()
        {
            EnabledButton(false);
            JObject jObject = Send("users.getFollowers", "count=1000", v);
            int count = jObject["response"]["count"].Value<int>();
            followersCount = followersCount == 0 ? count : followersCount;
            count = count > 1000 ? 1000 : count;
            for (int i = 0; i < count; i++)
            {
                int userId = jObject["response"]["items"][i].Value<int>();
                FollowersDelete(userId, ++this.i, followersCount);
            }
            if (count > 0)
                Followers();
            i = 0;
            followersCount = 0;
            WriteLog("Все подписчики удалены");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete member died or banned
        /// </summary>
        /// <param name="groupId">Group id</param>
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
                GroupsRemoveUser((int)groupId, list[j], j, count);
            WriteLog("Мертвые аккаунты удаленны");
            EnabledButton(true);
        }

        /// <summary>
        /// Delete all members
        /// </summary>
        /// <param name="groupId">Group id</param>
        public void GroupsMembers(object groupId)
        {
            EnabledButton(false);
            manager = manager.Length == 0 ? GroupsManagers(groupId, ref managerCount) : manager;
            JObject jObject = Send("groups.getMembers", "group_id=" + groupId, v);
            int count = jObject["response"]["count"].Value<int>() - managerCount;
            foreach (JToken current in jObject["response"]["items"])
            {
                int groupId2 = int.Parse(groupId.ToString());
                int userId = current.Value<int>();
                if (manager.IndexOf(userId.ToString(), StringComparison.Ordinal) < 0)
                    GroupsRemoveUser(groupId2, userId, i++, count);
            }
            if (count > 0)
                GroupsMembers(groupId);
            i = 0;
            manager = "";
            managerCount = 0;
            WriteLog("Подписчики удалены");
            EnabledButton(true);
        }
    }
}
