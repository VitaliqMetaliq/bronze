using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotVQ
{
    /// <summary>
    /// Класс сообщений, содержащий информацию о сообщении 
    /// и пользователе, который ее отправил
    /// </summary>
    class MessageLog
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ИД пользователя
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Дата получения
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userName">Имя</param>
        /// <param name="id">ИД</param>
        /// <param name="msg">Сообщение</param>
        /// <param name="date">Дата</param>
        public MessageLog(string userName, long id, string msg, DateTime date)
        {
            this.UserName = userName;
            this.Id = id;
            this.Message = msg;
            this.Date = date;
        }
    }
}
