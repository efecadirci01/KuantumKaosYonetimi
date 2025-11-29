# KuantumKaosYonetimi
Kuantum Kaos Yönetimi projesini hazırlarken, Nesne Yönelimli Programlama (OOP) prensiplerinin dört farklı dilde (C#, Java, Python, JavaScript) tutarlı bir şekilde uygulanmasını temel aldım. Projenin mimarisini,
tüm maddelerin ortak özelliklerini (ID, Stabilite) ve kapsülleme kurallarını içeren KuantumNesnesi soyut sınıfı (Abstract Class) üzerine inşa ettim. 
Tehlikeli maddelere özgü soğutma davranışlarını ayrıştırmak için IKritik arayüzünü sisteme entegre ettim. VeriPaketi, KaranlikMadde ve AntiMadde sınıflarını türeterek, 
her birinin AnalizEt metodunda farklı davranışlar sergilemesini Polimorfizm ile sağladım. Son olarak, simülasyonun kritik başarısızlık durumunu yönetmek için KuantumCokusu adında özel bir hata sınıfı geliştirerek,
çalışma zamanı hatalarını kontrollü bir oyun mantığına dönüştürdüm.
