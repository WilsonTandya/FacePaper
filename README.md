# Tugas Besar 2 - Strategi Algoritma
>  Pengaplikasian Algoritma *BFS* dan *DFS* dalam Fitur *People You May Know* Jejaring Sosial *Facebook*

### Kelompok 26 - *FacePaper*
| Anggota | NIM |
| --- | --- |
|Muhammad Dzaki Razaan Faza | 13519033 |	
|Joel Triwira| 13519073 |	
|Wilson Tandya | 13519209 |

## Penjelasan Singkat Algoritma yang Diimplementasikan
Pada fitur *explore friends* terdapat dua opsi algoritma yang dapat digunakan, *DFS* dan *BFS*. Pada fitur *friend recommendations* algoritma yang digunakan adalah *BFS*.

Pada algoritma *DFS*, program mencari indeks dari simpul awal, membuat simpul menjadi *visited*, memasukkan simpul awal ke dalam *stack*, mencari simpul berikutnya secara alfabetikal, bila simpul berikutnya bukan merupakan solusi, akan diulangi kembali proses hingga mendapatkan solusi / seluruh simpul telah dikunjungi.

Pada algoritma *BFS*, program mencari indeks dari simpul awal, membuat simpul menjadi *visited*, memasukkan simpul awal ke dalam *queue*, mengeluarkan simpul pertama dalam *queue* dan memasukkan semua tetanga dari simpul tersebut, cek apakah simpul merupkana solusi, bila belum akan diulangi proses hingga mendapatkan solusi / seluruh simpul telah dikunjungi.

## Cara Menggunakan Program
Menjalankan `Visualizer.exe`pada folder bin

## Requirement untuk Kompilasi Program
* Kami menggunakan Visual Studio Community Edition 2019 dalam membuat program ini, dengan membuka `Visualizer.sln` pada folder src dan menekan `F5` program akan ter-*compile* dan langsung dapat digunakan.
* Program dibuat dengan bahasa pemrograman C# dengan kakas Visual Studio .NET
## Struktur Folder
```
FacePaper
│
├── src
│   └── Visualizer/         [ Kode `.cs` ada disini ]
│       └── ... .cs
│       └── ... 
│   └── Visualizer.sln      [ `.sln` ]
│   └── ...
│
├── bin/                    
│   └── Visualizer.exe      [ Executable ]
│   └── ...
│
├── test/                   [ Input File Eksternal ]
│   └── ... .txt
├── doc
│   └── FacePaper.pdf
│
└── README.md
```
