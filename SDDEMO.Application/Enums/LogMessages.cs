using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Enums
{
    public enum LogMessages
    {
        [Description("An error occured. Error message: {errorMessage}. StackTrace: {stackTrace}")]
        LoggingMessageForError,

        [Description("{entity} kaydı veritabanına eklendi. Guid: {guid}")]
        InsertDatabase,

        [Description("{entity} kaydı veritabanında güncellendi. Guid: {guid}")]
        UpdateDatabase,

        [Description("{entity} kaydı veritabanından silindi. Guid: {guid}")]
        DeleteDatabase,

        [Description("{entity} kaydı veritabanından alındı. Guid: {guid}")]
        GetDatabase,

        [Description("{entity} listesi veritabanından alındı.")]
        GetAllDatabase,
    }
}
