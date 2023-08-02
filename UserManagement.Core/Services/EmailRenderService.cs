namespace UserManagement.Core.Services;

public class EmailRenderService : IEmailRenderService
{
    public async Task<string> RenderEmailTemplate(string userName, string activationLink, string companyName)
    {
        // Mail içeriği şablonunu doldurarak HTML içeriğini oluşturma
        var htmlTemplate = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Hesap Aktivasyonu</title>
 <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
        }
        .logo {
            text-align: center;
            margin-bottom: 20px;
        }
        .logo img {
            max-width: 150px;
        }
        .message {
            padding: 15px;
            background-color: #f0f0f0;
            border-radius: 5px;
        }
        .activation-link {
            display: inline-block;
            margin-top: 20px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 5px;
        }
        .footer {
            margin-top: 30px;
            text-align: center;
            color: #888;
        }
    </style>
</head>
<body>
    <div class='container'>
        <div class='logo'>
            <img src='https://example.com/logo.png' alt='Logo'>
        </div>
        <div class='message'>
            <p>Merhaba [KULLANICI_AD],</p>
            <p>Hesabınızı aktifleştirmek için aşağıdaki düğmeye tıklayın:</p>
            <a class='activation-link' href='[AKTIVASYON_LINKI]'>Hesabımı Aktifleştir</a>
        </div>
        <div class='footer'>
            <p>Eğer düğmeye tıklama ile ilgili bir problem yaşarsanız, aşağıdaki bağlantıyı tarayıcınızın adres çubuğuna kopyalayın:</p>
            <p>[AKTIVASYON_LINKI]</p>
            <p>Bu e-posta, [SIRKET_AD] tarafından otomatik olarak gönderilmiştir. Lütfen cevaplamayın.</p>
        </div>
    </div>
</body>
</html>";

        // Değişkenleri şablon içindeki değerlerle değiştirme
        htmlTemplate = htmlTemplate.Replace("[KULLANICI_AD]", userName);
        htmlTemplate = htmlTemplate.Replace("[AKTIVASYON_LINKI]", activationLink);
        htmlTemplate = htmlTemplate.Replace("[SIRKET_AD]", companyName);

        return await Task.FromResult<string>(htmlTemplate);
    }
}

public interface IEmailRenderService
{
    Task<string> RenderEmailTemplate(string userName, string activationLink, string companyName);
}