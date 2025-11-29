import random
from abc import ABC, abstractmethod


class KuantumCokusuException(Exception):
    def __init__(self, nesne_id):
        self.message = f"KRİTİK HATA: Nesne {nesne_id} patladı! Sistem Çöktü."
        super().__init__(self.message)


class IKritik(ABC):
    @abstractmethod
    def acil_durum_sogutmasi(self):
        pass


class KuantumNesnesi(ABC):
    def __init__(self, id, stabilite, tehlike_seviyesi):
        self._id = id
        self._tehlike_seviyesi = tehlike_seviyesi
        self._stabilite = 100
        self.stabilite = stabilite 

    @property
    def id(self):
        return self._id

    @property
    def stabilite(self):
        return self._stabilite

    @stabilite.setter
    def stabilite(self, value):
        if value > 100:
            self._stabilite = 100
        elif value <= 0:
            self._stabilite = 0
            
            raise KuantumCokusuException(self._id)
        else:
            self._stabilite = value

    @abstractmethod
    def analiz_et(self):
        pass

    def durum_bilgisi(self):
        return f"ID: {self._id} | Stabilite: %{self._stabilite} | Tehlike: {self._tehlike_seviyesi}"


class VeriPaketi(KuantumNesnesi):
    def __init__(self, id):
        super().__init__(id, 100, 1)

    def analiz_et(self):
        self.stabilite -= 5
        print(">> Veri içeriği okundu.")

class KaranlikMadde(KuantumNesnesi, IKritik):
    def __init__(self, id):
        super().__init__(id, 100, 7)

    def analiz_et(self):
        self.stabilite -= 15
        print(">> Karanlık madde analiz edildi.")

    def acil_durum_sogutmasi(self):
        try:
            self.stabilite += 50
            print(f">> {self.id} soğutuldu. Yeni Stabilite: {self.stabilite}")
        except KuantumCokusuException:
            pass 

class AntiMadde(KuantumNesnesi, IKritik):
    def __init__(self, id):
        super().__init__(id, 100, 10)

    def analiz_et(self):
        self.stabilite -= 25
        print(">> UYARI: Evrenin dokusu titriyor...")

    def acil_durum_sogutmasi(self):
        try:
            self.stabilite += 50
            print(f">> {self.id} (Anti-Madde) soğutuldu.")
        except KuantumCokusuException:
            pass


def main():
    envanter = []
    sayac = 1
    sistem_calisiyor = True

    print("--- KUANTUM AMBARI ---")

    while sistem_calisiyor:
        print("\n1. Yeni Nesne Ekle")
        print("2. Tüm Envanteri Listele")
        print("3. Nesneyi Analiz Et")
        print("4. Acil Durum Soğutması Yap")
        print("5. Çıkış")
        secim = input("Seçiminiz: ")

        try:
            if secim == "1":
                yeni_id = f"NESNE-{1000 + sayac}"
                sayac += 1
                tip = random.choice([VeriPaketi, KaranlikMadde, AntiMadde])
                nesne = tip(yeni_id)
                envanter.append(nesne)
                print(f"Eklendi: {type(nesne).__name__}")

            elif secim == "2":
                for n in envanter:
                    print(f"{n.durum_bilgisi()} [{type(n).__name__}]")

            elif secim == "3":
                id_gir = input("ID Giriniz: ")
                bulundu = False
                for n in envanter:
                    if n.id == id_gir:
                        n.analiz_et()
                        bulundu = True
                        break
                if not bulundu: print("Nesne bulunamadı.")

            elif secim == "4":
                id_gir = input("ID Giriniz: ")
                bulundu = False
                for n in envanter:
                    if n.id == id_gir:
                        
                        if isinstance(n, IKritik):
                            n.acil_durum_sogutmasi()
                        else:
                            print("HATA: Bu nesne soğutulamaz!")
                        bulundu = True
                        break
                if not bulundu: print("Nesne bulunamadı.")

            elif secim == "5":
                sistem_calisiyor = False
                print("Çıkış yapıldı.")
            
        except KuantumCokusuException as e:
          
            print("SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR...")
            print(e)
        
            sistem_calisiyor = False

if __name__ == "__main__":
    main()