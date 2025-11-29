import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.Scanner;

// D. Özel Hata Yönetimi (Custom Exception)
class KuantumCokusuException extends Exception {
    public KuantumCokusuException(String id) {
        super("KRİTİK HATA: Nesne " + id + " stabilitesini kaybetti!");
    }
}

// B. Arayüz (Interface Segregation)
interface IKritik {
    void acilDurumSogutmasi();
}

// A. Temel Yapı (Abstract Class & Encapsulation)
abstract class KuantumNesnesi {
    private String id;
    private int stabilite;
    private int tehlikeSeviyesi;

    public KuantumNesnesi(String id, int stabilite, int tehlikeSeviyesi) throws KuantumCokusuException {
        this.id = id;
        this.tehlikeSeviyesi = tehlikeSeviyesi;
        // Setter kullanarak kontrolü sağlıyoruz
        setStabilite(stabilite);
    }

    public String getId() { return id; }

    public int  getStabilite() { return stabilite; }

    // Kapsülleme: Stabilite kontrolü
    public void setStabilite(int value) throws KuantumCokusuException {
        if (value > 100) {
            this.stabilite = 100;
        } else if (value <= 0) {
            this.stabilite = 0;
            throw new KuantumCokusuException(this.id);
        } else {
            this.stabilite = value;
        }
    }

    public int getTehlikeSeviyesi() { return tehlikeSeviyesi; }

    // Abstract metod
    public abstract void analizEt() throws KuantumCokusuException;

    public String durumBilgisi() {
        return "ID: " + id + " | Stabilite: %" + String.format( ""+ stabilite) + " | Tehlike: " + tehlikeSeviyesi;
    }
}

// C. Nesne Çeşitleri (Inheritance & Polymorphism)

class VeriPaketi extends KuantumNesnesi {

    public VeriPaketi(String id) throws KuantumCokusuException {
        super(id, 100, 1);
    }

    @Override
    public void analizEt() throws KuantumCokusuException {
        setStabilite(getStabilite() - 5);
        

        System.out.println(">> Veri içeriği okundu.");
    }
}

class KaranlikMadde extends KuantumNesnesi implements IKritik {
    public KaranlikMadde(String id) throws KuantumCokusuException {
        super(id, 100, 7); // Varsayılan değerler
    }

    @Override
    public void analizEt() throws KuantumCokusuException {
        setStabilite(getStabilite() - 15);
        System.out.println(">> Karanlık madde analiz edildi.");
    }

    @Override
    public void acilDurumSogutmasi() {
        try {
            setStabilite(getStabilite() + 50);
            System.out.println(">> Soğutma başarılı! Stabilite arttı.");
        } catch (KuantumCokusuException e) {
            // Soğutma sırasında çökme olması imkansız ama try-catch zorunlu
        }
    }
}

class AntiMadde extends KuantumNesnesi implements IKritik {
    public AntiMadde(String id) throws KuantumCokusuException {
        super(id, 100, 10);
    }

    @Override
    public void analizEt() throws KuantumCokusuException {
        setStabilite(getStabilite() - 25);
        System.out.println(">> UYARI: Evrenin dokusu titriyor...");
    }

    @Override
    public void acilDurumSogutmasi() {
        try {
            setStabilite(getStabilite() + 50);
            System.out.println(">> Anti-Madde stabilize edildi.");
        } catch (KuantumCokusuException e) { }
    }
}


public class KuantumKaosYönetimi {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        List<KuantumNesnesi> envanter = new ArrayList<>();
        Random rnd = new Random();
        int sayac = 1;

        System.out.println("--- KUANTUM KAOS YÖNETİMİ ---");
        System.out.println("Vardiya Başlıyor...");

        boolean sistemCalisiyor = true;

        while (sistemCalisiyor) {
            System.out.println("\n--- KONTROL PANELİ ---");
            System.out.println("1. Yeni Nesne Ekle");
            System.out.println("2. Tüm Envanteri Listele");
            System.out.println("3. Nesneyi Analiz Et");
            System.out.println("4. Acil Durum Soğutması Yap");
            System.out.println("5. Çıkış");
            System.out.print("Seçiminiz: ");

            String secim = scanner.nextLine();

            try {
                switch (secim) {
                    case "1":
                        int tur = rnd.nextInt(3);
                        KuantumNesnesi yeniNesne = null;
                        String id = "NESNE-" + (1000 + sayac++);
                        
                        if (tur == 0) yeniNesne = new VeriPaketi(id);
                        else if (tur == 1) yeniNesne = new KaranlikMadde(id);
                        else yeniNesne = new AntiMadde(id);
                        
                        envanter.add(yeniNesne);
                        System.out.println("Yeni nesne kabul edildi: " + yeniNesne.getClass().getSimpleName());
                        break;

                    case "2":
                        if(envanter.isEmpty()) System.out.println("Depo boş.");
                        for (KuantumNesnesi kn : envanter) {
                            System.out.println(kn.durumBilgisi() + " [" + kn.getClass().getSimpleName() + "]");
                        }
                        break;

                    case "3":
                        System.out.print("Analiz edilecek ID: ");
                        String analizId = scanner.nextLine();
                        boolean analizBulundu = false;
                        for (KuantumNesnesi kn : envanter) {
                            if (kn.getId().equals(analizId)) {
                                kn.analizEt(); 
                                analizBulundu = true;
                                break;
                            }
                        }
                        if (!analizBulundu) System.out.println("Nesne bulunamadı.");
                        break;

                    case "4":
                        System.out.print("Soğutulacak ID: ");
                        String sogutmaId = scanner.nextLine();
                        boolean sogutmaBulundu = false;
                        for (KuantumNesnesi kn : envanter) {
                            if (kn.getId().equals(sogutmaId)) {
                              
                                if (kn instanceof IKritik) {
                                    ((IKritik) kn).acilDurumSogutmasi();
                                } else {
                                    System.out.println("HATA: Bu nesne (VeriPaketi) soğutulamaz!");
                                }
                                sogutmaBulundu = true;
                                break;
                            }
                        }
                        if (!sogutmaBulundu) System.out.println("Nesne bulunamadı.");
                        break;

                    case "5":
                        sistemCalisiyor = false;
                        System.out.println("Vardiya bitti.");
                        break;
                        
                    default:
                        System.out.println("Geçersiz işlem.");
                }
            } catch (KuantumCokusuException e) {
              
                System.out.println("SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR...");
                System.out.println("Sebep: " + e.getMessage());
                
                sistemCalisiyor = false; 
            }
        }
        scanner.close();
    }
}