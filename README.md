# Fast Food Sipariş Otomasyonu
10. Sınıf NTP Dönem Projesi

***Bu proje nedir?***

Repo'da yazdığı gibi bu bir yemek sipariş otomasyonu(tamamen bitmemiştir). Mobil uygulama ve WindowsFormApp Desteği bulunmakta.

## SSS (Sıkça Sorulan Sorular)


***Api'yi nasıl çalıştıracağım?***


Api yi çalıştırmak için FoodApi'deki projeyi açıp üst taraftaki 'IIS Express' e tıklamak ve açılan tarayıcı sekmesini kapatmamak yeterlidir.

***Api'yi sadece başlatmak yeterli mi?***

Api'yi sadece başlatmak yeterli değil. Api'yi mobil uygulamada yerel ağ dışı kullanmak için bir Visual Studio eklentisi kurmanız gerekmektedir. Eklentiyi kurmak için FoodApi'deki projeyi açıp Visual Studio'daki Uzantılar menüsünü açıp 'Uzantıları Yönet' e tıklıyoruz burada çevrimiçi bölümünden 'Conveyor by Keyoti for VS 2022+' uzantısını Visual Studio ya ekliyoruz sonra, Api'yi başlatıp indirdiğimiz uzantıdan 'Access Over Internet yazan butona basıyoruz. Çıkan Internet URL yi kaydedip mobil uygulamanın olduğu projeyi açıyoruz. AppSettings.cs deki 'public static string ApiUrl' yazan saturu kaydettiğimiz URL ile değiştiriyoruz.

***SQL Server Error hatası alıyorum, bunu nasıl çözerim?***

Sql Server Error hatasını çözmek için ilk önce Sql Server indirmeniz gerekmektedir. [Buradan](https://www.youtube.com/watch?v=AO0u0D6pKP4) nasıl indirileceğini görebilirsiniz. Sql Server'ı indirdikten sonra, SSMS(SQL Server Management Studio)'dan 'Databases' a sağ tıklayıp 'Restore Database' e tıklıyoruz. Vermiş olduğum 'Otomasyon.bak' buradan seçip OK diyoruz ve Windows Form App in projesini açıyoruz. Projedeki her bir WindowsForm dosyasını açıp 'static string baglanti' yazan satırı kendi bağlantı satırımız ile değiştiriyoruz.

### Yapılacaklar


#### Kontrol Listesi
- [ ] Windows Form Uygulması bitirilecek.
- [x] Mobil Uygulama bitirilecek.

#### Windows Form
- [x] Yönetici ve Çalışan giriş ekranı
- [x] Çalışan sipariş oluşturma
- [ ] Çalışan gazete, radyo
- [ ] Aşçı sipariş paneli
   - [x] Çalışanların oluşturduğu siparişin durumunu değiştirme
   - [ ] Mobil uygulamadan oluşturulan Siparişlerin durumunu değiştirme
- [ ] Yönetici yönetme paneli
   - [x] Çalışan yönetme
   - [x] Menü düzenleme
   - [ ] Mobil uygulama menü düzenleme
- [ ] Bütün panellere yardım(i) butonu ve videoları

#### Mobil Uygulama
- [x] Müşteri giriş ekranı
- [x] Müşteri kayıt ekranı
- [x] Menülere özel sipariş ekranı
    - [x] Sepete sipariş ekleme
    - [x] Sipariş adet değiştirme
- [x] Sepet
    - [x] Sipariş verme
    - [x] Sepeti temizleme
- [x] Grid yan menü
   - [x] Önceki oluşturulan siparişler
      - [x] Siparişlerin içeriğini görüntüleme
   - [x] Sepet
   - [x] Çıkış yap
   - [x] Bize ulaşın
