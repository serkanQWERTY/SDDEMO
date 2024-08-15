using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Enums
{
    public enum ResponseMessages
    {
        [Description("Bir hata oluştu.")]
        AnErrorOccured,

        [Description("Kayıt başarılı.")]
        SuccessfullyCreated,

        [Description("Güncelleme başarılı.")]
        SuccessfullyUpdated,

        [Description("{countOfRecords} adet kayıt bulundu.")]
        RecordsFound,

        [Description("Kayıt bulunamadı.")]
        RecordNotFound,

        [Description("Silme işlemi başarılı.")]
        SuccessfullyDeleted,

        [Description("Kullanıcı adı veya şifre hatalı.")]
        UserNotFound,

        [Description("İşlem başarılı.")]
        SuccessfullyDone,

        [Description("Durum başarıyla değiştirildi.")]
        SuccessfullyChanged,

        [Description("Kullanıcı bulunamadı.")]
        UserTokenError,

        [Description("Geçersiz değer.")]
        InvalidValue,

        [Description("Kullanıcı zaten mevcut.")]
        UserAlreadyExists
    }
}
