using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vk.Clear
{
    partial class Work
    {
        private readonly string v;

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

        public bool DelSavePhoto { get; set; }

        public bool DelAlbumVideo { get; set; }

        public bool DelBanned { get; set; }

        public bool DelWallPhotoGroup { get; set; }

        /// <summary>
        /// send user delete of fave
        /// </summary>
        /// <param name="user_id">User id</param>
        /// <returns></returns>
        private int RemoveFaveUser(int user_id)
        {
            Thread.Sleep(500);
            JObject jObject = Send("fave.removeUser", "user_id=" + user_id);
            return jObject["response"].Value<int>();
        }

        /// <summary>
        /// send delete link of fave
        /// </summary>
        /// <param name="link_id">Link id</param>
        /// <returns>response if one then deleted</returns>
        private int RemoveLink(string link_id)
        {
            Thread.Sleep(500);
            JObject jObject = Send("fave.removeLink", "link_id=" + link_id, v);
            return jObject["response"].Value<int>();
        }

        /// <summary>
        /// Like delete of post or set type
        /// </summary>
        /// <param name="type">Type post</param>
        /// <param name="owner_id">Owner id</param>
        /// <param name="item_id">Item id</param>
        /// <returns>if deleted then returns 1</returns>
        private int LikesDelete(string type, int owner_id, int item_id)
        {
            Thread.Sleep(500);
            JObject jObject = Send("likes.delete",
                String.Format("type={0}&owner_id={1}&item_id={2}",
                type, owner_id, item_id), v);
            return jObject["response"]["likes"].Value<int>();
        }

        /// <summary>
        /// Clear album for wall
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        private void PhotosWall(string ownerId)
        {
            Thread.Sleep(1000);
            JObject jObject = Send("photos.get", "owner_id=" + ownerId + "&album_id=wall&count=200&rev=1");
            int num = jObject["response"]["count"].Value<int>();
            photoCount = photoCount == 0 ? num : photoCount;
            num = num > 200 ? 200 : num;
            for (int i = 1; i < num; i++)
            {
                int ownerIdPw = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int photoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotoDelete(ownerIdPw, photoId, ++this.i, photoCount);
            }
            if (num > 0 && i < 1000)
                PhotosWall(ownerId);
            i = 0;
            photoCount = 0;
            WriteLog("Удалили (максимум 1000) за раз");
        }

        /// <summary>
        /// Get died or banned users in group
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <param name="deletedUsers">list storage died users</param>
        /// <param name="offset">Offset</param>
        /// <returns>JSON response</returns>
        private JObject GetMembers(object groupId, ref List<int> deletedUsers, int offset)
        {
            Thread.Sleep(500);
            JObject jObject = Send("groups.getMembers", param: string.Concat("group_id=", groupId, "&offset=", offset, "&fields=first_name"));
            AddDeletedUsers(ref deletedUsers, jObject);
            return jObject;
        }

        /// <summary>
        /// Add to list user died
        /// </summary>
        /// <param name="del">list</param>
        /// <param name="get">JSON repsonse</param>
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

        /// <summary>
        /// Delete member
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <param name="userId">User id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void GroupsRemoveUser(int groupId, int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("groups.removeUser", string.Concat("group_id=", groupId, "&user_id=", userId), v);
            WriteLog(string.Concat("Удалено подписчиков ", i, " из ", count));
        }


        /// <summary>
        /// Get manager in group
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <param name="count">Count members</param>
        /// <returns>string mangers id split ,</returns>
        private string GroupsManagers(object groupId, ref int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
            JObject jObject = Send("groups.getMembers", "group_id=" + groupId + "&filter=managers", v);
            string text = "";
            count = jObject["response"]["count"].Value<int>();
            for (int i = 0; i < count; i++)
                text = text + jObject["response"]["items"][i]["id"] + ",";
            return text.Substring(0, text.Length - 1);
        }

        /// <summary>
        /// Send delete (add ban) follower
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void FollowersDelete(int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("account.banUser", "user_id=" + userId, v);
            WriteLog(string.Concat("Удаленно подписчиков ", i, " из ", count));
        }

        /// <summary>
        /// Send delete doc
        /// </summary>
        /// <param name="docId">Doc id</param>
        /// <param name="ownerId">Doc owner id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void DocsDelete(int docId, int ownerId, int i, int count)
        {
            Thread.Sleep(500);
            Send("docs.delete", string.Concat("owner_id=", ownerId, "&doc_id=", docId), v);
            WriteLog(string.Concat("Удалено документов ", i, " из ", count));
        }

        /// <summary>
        /// Send delete note
        /// </summary>
        /// <param name="noteId">Note id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void NotesDelete(int noteId, int i, int count)
        {
            Thread.Sleep(500);
            Send("notes.delete", "note_id=" + noteId, v);
            WriteLog(string.Concat("Удалено заметок ", i, " из ", count));
        }

        /// <summary>
        /// Send delete topic
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <param name="topicId">Topic id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void BoardDeleteTopic(int groupId, int topicId, int i, int count)
        {
            Thread.Sleep(500);
            Send("board.deleteTopic", string.Concat("group_id=", groupId, "&topic_id=", topicId), v);
            WriteLog(string.Concat("Удалено обсуждений ", i, " из ", count));
        }

        /// <summary>
        /// Send delete user of blacklist
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void AccountUnban(int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("account.unbanUser", "user_id=" + userId, v);
            WriteLog(string.Concat("Убранно из бана: ", i, " из ", count));
        }

        /// <summary>
        /// Send delete news list
        /// </summary>
        /// <param name="listId">list id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void NewsfeedDelete(int listId, int i, int count)
        {
            Thread.Sleep(500);
            Send("newsfeed.deleteList", "list_id=" + listId, v);
            WriteLog(string.Concat("Удалено новостных списков ", i, " из ", count));
        }

        /// <summary>
        /// Get count message history
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Count</returns>
        private int MessageHistory(int userId)
        {
            Thread.Sleep(1000);
            JObject jObject = Send("messages.getHistory", "user_id=" + userId, v);
            return jObject["response"]["count"].Value<int>();
        }

        /// <summary>
        /// Send delete dialog
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="chatId">Dialog id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void MessagesDelete(int userId, int chatId, int i, int count)
        {
            Thread.Sleep(500);
            int dialogId = chatId > 0 ? chatId : userId;
            if (chatId > 0)
                Send("messages.deleteDialog", "chat_id=" + dialogId, v);
            else
                Send("messages.deleteDialog", "user_id=" + dialogId, v);
            if (userId > 0 && MessageHistory(userId) > 10000 && chatId < 0)
                MessagesDelete(userId, chatId, i, count);
            WriteLog(string.Concat("Удалено диалогов ", i, " из ", count));
        }

        /// <summary>
        /// Delete video albums
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        private void VideoAlbum(object ownerId)
        {
            Thread.Sleep(500);
            JObject jObject = Send("video.getAlbums", "owner_id=" + ownerId + "&count=100", v);
            int count = jObject["response"]["count"].Value<int>();
            videoCount = videoCount == 0 ? count : videoCount;
            count = count > 100 ? 100 : count;
            for (int i = 0; i < count; i++)
            {
                int ownerId2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int albumId = jObject["response"]["items"][i]["id"].Value<int>();
                VideoAlbumDelete(ownerId2, albumId, ++this.i, videoCount);
            }
            if (count > 0)
                VideoAlbum(ownerId);
            i = 0;
            videoCount = 0;
            WriteLog("Видео и видеоальбомы удалены");
        }

        /// <summary>
        /// Send delete video album
        /// </summary>
        /// <param name="ownerId">Owner id album</param>
        /// <param name="albumId">Album id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void VideoAlbumDelete(int ownerId, int albumId, int i, int count)
        {
            int num = ownerId < 0 ? Math.Abs(ownerId) : 0;
            Send("video.deleteAlbum", string.Concat("group_id=", num, "&album_id=", albumId), v);
            WriteLog(string.Concat("Удалено видеоальбомов ", i, " из ", count));
        }

        /// <summary>
        /// Send delete video
        /// </summary>
        /// <param name="videoId">Video id</param>
        /// <param name="ownerId">Owner id video</param>
        /// <param name="targetId">Traget id: where storage video</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void VideoDelete(int videoId, int ownerId, int targetId, int i, int count)
        {
            Thread.Sleep(500);
            Send("video.delete", string.Concat("video_id=", videoId, "&owner_id=", ownerId, "&target_id=", targetId), v);
            WriteLog(string.Concat("Удалено видео ", i, " из ", count));
        }

        /// <summary>
        /// Delete photo type: saved
        /// </summary>
        private void PhotosSaved()
        {
            JObject jObject = Send("photos.get", "album_id=saved", v);
            int count = jObject["response"]["count"].Value<int>();
            photoCount = photoCount == 0 ? count : photoCount;
            count = count >= 1000 ? 1000 : count;
            for (int i = 0; i < count; i++)
            {
                int ownerId2 = jObject["response"]["items"][i]["owner_id"].Value<int>();
                int phtoId = jObject["response"]["items"][i]["id"].Value<int>();
                PhotoDelete(ownerId2, phtoId, ++this.i, photoCount, "Удалено сохраненых фотографий ");
            }
            if (count > 0)
                PhotosSaved();
            WriteLog("Сохраненые фотографии удалены");
            i = 0;
            photoCount = 0;
        }

        /// <summary>
        /// Send delete album 
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        /// <param name="albumId">Album id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void PhotosAlbumDelete(int ownerId, int albumId, int i, int count)
        {
            Thread.Sleep(500);
            int num = ownerId < 0 ? ownerId : 0;
            Send("photos.deleteAlbum", string.Concat("album_id=", albumId, "&group_id=", num), v);
            WriteLog(string.Concat("Удаленно альбомов ", i, " из ", count));
        }

        /// <summary>
        /// Send delete photo
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        /// <param name="photoId">Photo id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        /// <param name="text">Message info</param>
        private void PhotoDelete(int ownerId, int photoId, int i, int count, string text = "Удалено фотографий ")
        {
            Thread.Sleep(1000);
            Send("photos.delete", string.Concat("owner_id=", ownerId, "&photo_id=", photoId), v);
            WriteLog(string.Concat(text, i, " из ", count));
        }

        /// <summary>
        /// Send delete friend
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void FriendsDelete(int userId, int i, int count)
        {
            Thread.Sleep(1000);
            Send("friends.delete", "user_id=" + userId, v);
            WriteLog(string.Concat("Удаленно друзей ", i, " из ", count));
        }

        /// <summary>
        /// Send delete of ban user in group
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <param name="userId">User id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void GroupsUnbanUser(int groupId, int userId, int i, int count)
        {
            Thread.Sleep(500);
            Send("groups.unbanUser", string.Concat("group_id=", groupId, "&user_id=", userId), v);
            WriteLog(string.Concat("Убрано из бана ", i, " из ", count));
        }

        /// <summary>
        /// Leave of group
        /// </summary>
        /// <param name="groupId">Group id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void LeaveGroups(int groupId, int i, int count)
        {
            Thread.Sleep(1000);
            Send("groups.leave", "group_id=" + groupId, v);
            WriteLog(string.Concat("Покинуто ", i, " из ", count));
        }

        /// <summary>
        /// Send delete post in wall
        /// </summary>
        /// <param name="ownerId">Group or user id</param>
        /// <param name="postId">Post id</param>
        /// <param name="i">Number in write log</param>
        /// <param name="count">All count</param>
        private void WallDelete(int ownerId, int postId, int i = 0, int count = 0)
        {
            Thread.Sleep(500);
            Send("wall.delete", string.Concat("owner_id=", ownerId, "&post_id=", postId), v);
            WriteLog(string.Concat("Удалено записей ", i, " из ", count));
        }

        /// <summary>
        /// Write message in status
        /// </summary>
        /// <param name="text">Message</param>
        private void WriteLog(string text)
        {
            form.SetTextLabel(text);
        }

        /// <summary>
        /// Enable or disable button
        /// </summary>
        /// <param name="btnEnabled"></param>
        private void EnabledButton(bool btnEnabled)
        {
            form.SetButtonEnabled(btnEnabled);
        }
    }
}
