
# Moh Torikul Azis - Backend Developer (Freelance)

  
**Movie Services**
  
  

## Memulai Cepat

  

Cara memulai Pengembangan Lokal :

  

1. Pulihkan Solusi

- Buka Command Prompt, dan temukan folder root proyek ini

- Ketik `dotnet restore`,

- Lalu tekan Enter

2. Mulai layanan mysql **versi 8** atau yang terbaru

- Untuk memeriksa versi mysql Anda, jalankan sql ini `SHOW VARIABLES LIKE 'version';`

3. Tunjukkan string koneksi Anda ke layanan Anda di `appsettings.Common.Debug.json` yang terletak di `src/movie-api/`

- Anda dapat memisahkan appsettings antara admin-api dan gameserver-api dengan membuat `appsettings.Debug.Json` di `src/Movie.Api` 

4. Konfigurasi launchSetting.json jika diperlukan

5. Konfigurasi [User Secret](docs/configurations.md)

6. Mulai server Anda

- menggunakan Visual Studio :

- atur proyek Startup ke layanan apa yang ingin Anda jalankan

- pilih profil pengaturan peluncuran ke `Movie.Api-Services`. **bukan IIS Express**

- menggunakan CLI, `dotnet watch run`

# User Secret
Berbeda dengan appsettings, user secrets seharusnya menyimpan konfigurasi yang sensitif untuk proyek seperti kata sandi atau kunci rahasia atau sebagainya. Rahasia ini akan dipasang secara lokal pada mesin komputer dan tidak dibangun dengan kode sumber. <br/>Contoh paling sederhana adalah bahwa `appsettings akan didorong ke git (karena merupakan bagian dari kode sumber) sehingga nilainya dapat terungkap, tetapi user secrets tidak`. Inilah cara kita dapat menyembunyikan konfigurasi sensitif kita dengan aman.

## How to
Pada service ini ada beberapa konfigurasi yang perlu kita sembunyikan. Anda dapat melihat di `secret.json.TEMPLATE.txt` di `Movie.Api` untuk melihat daftar dan formatnya.<br/>
Ada beberapa cara untuk mengatur user secrets.
- Jika Anda menggunakan Visual Studio IDE 
    - Klik kanan pada `Movie.Api` dan pilih Manage User Secrets <br/>
        *ini akan membuka file yang disebut `secrets.json`* 
    - Salin dan tempel konfigurasi dari `secrets.json.TEMPLATE.txt` ke dalam file tersebut
    - Jangan lupa ubah nilainya
    - Kemudian Simpan
    
- Jika Anda seorang *pecinta* Visual Studio Code
    - Menggunakan CLI 
        - Buka CMD yang berlokasi di jalur  `Movie.Api`
        - Ketik `dotnet user-secrets list` lalu tekan Enter.<br/>
        Anda seharusnya tidak melihat rahasia yang dikonfigurasi untuk aplikasi, karena memang begitu. Anda perlu menambahkan konfigurasi satu per satu 
        - Yang perlu Anda atur adalah seperti di bawah ini : 
            - `dotnet user-secrets set "ConnectionStrings:DbPassword" "5rPQvkBWjFBEpzjyYDBBvWV4F_yrBH"`
            - `dotnet user-secrets set "MovieServerSecret:ChecksumKey" "XXX"`
            - `dotnet user-secrets set "MovieServerSecret:JWTSecret" "abcdefghijklmnoprstuvwxyz11234567890"`
        <br/> seperti yang Anda lihat perintah `dotnet user secrets set` mengambil dua parameter, parameter pertama adalah untuk nama/kunci konfigurasi dan parameter kedua adalah nilainya. Jadi Anda perlu mengubah semua nilai menjadi sesuatu yang lain. Jangan set dengan nilai semu ini. *Anda hanya perlu membuat string acak lalu lakukan perintah di atas, itu akan menimpa konfigurasi.*
        - Setelah Anda selesai mengaturnya semua, ketik lagi `dotnet user-secrets list`, dan daftarnya akan seperti ini
        ```
        MovieServerSecret:JWTSecret = abcdefghijklmnoprstuvwxyz11234567890
        MovieServerSecret:ChecksumKey = XXX
        ConnectionStrings:DbPassword = 5rPQvkBWjFBEpzjyYDBBvWV4F_yrBH
        ```
    - Menggunakan Ekstensi Visual Studio *(yang sederhana)*
        - Buka Ekstensi Visual Studio Code (`Ctrl+Shift+X`)
        - Cari `.Net Core User Secrets` oleh *Adrian Wilczyńsk*
        - Kemudian klik instal
        - Sekarang Anda dapat dengan mudah klik kanan pada `Movie.Api.csproj` lalu klik Manage User Secret
        - Salin dan tempel konfigurasi dari `secrets.json.TEMPLATE.txt` ke dalam file tersebut
        - Jangan lupa ubah nilainya
        - Kemudian Simpanlah

Berikut adalah nilai appSetting yang perlu disimpan di user secret manager

```json

{

    "ConnectionStrings": {

        "DbPassword": "password kamu",
    },

    "RabbitMqServer": {

        "HostName": "localhost",

        "UserName": "guest",

        "Password": "guest",

        "Port": 5672

    },

    "Authority": {

        "ClientId": "mp.583e5ddbf33c46f5ab1dfaafv9132435",

        "ClientSecret": "mp.635c3ha9u9623cevbca57790e3ba7c34",

        "ClientScope": "server"

    }

}



## List API
**User API**
[POST] Register User

**Genre**
[POST] Create Genre (Admin only)
[PUT] Edit Genre (Admin only)
[GET] Genre List
[GET] Genre Detail

**Movie**
[POST] Create Movie (Admin only)
[PUT] Edit Movie (Admin only)
[DELETE] Delete Movie (Admin only)
[GET] Movie List
[GET] Movie Detail

**User Review**
[POST] Post Review
[PUT] Edit Review
[DELETE] Delete Review 
[GET] My Review List
[GET] My Movie Detail

Jalankan API Register ketika anda baru memulai aplikasi ini, karena user anda (dari Mythic Account) belum terdaftar di Movie Service.

Untuk login Gunakan Kredensial berikut : 
**User Biasa**
Email :  thoriqul.aziz29@gmail.com
Password : Bagibagi123!
atau anda bisa gunakan akun sendiri dengan cara Register di Mythic Account

**Admin** 
Email : torikul@agate.id
Password : Bagibagi123!


## Lainnya

>  **[Link Sertifikat Hackerrank](https://www.hackerrank.com/certificates/29ec7e0c4d21)**