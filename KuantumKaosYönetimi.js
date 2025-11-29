const readline = require('readline');

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});


const soruSor = (soru) => new Promise(resolve => rl.question(soru, resolve));


class KuantumCokusuException extends Error {
    constructor(id) {
        super(`KRİTİK HATA: Nesne ${id} stabilitesini kaybetti!`);
        this.name = "KuantumCokusuException";
    }
}


class KuantumNesnesi {
    constructor(id, stabilite, tehlikeSeviyesi) {
        if (this.constructor === KuantumNesnesi) {
            throw new Error("Abstract sınıf new ile oluşturulamaz.");
        }
        this._id = id;
        this._tehlikeSeviyesi = tehlikeSeviyesi;
        this.setStabilite(stabilite);
    }

    getId() { return this._id; }

    getStabilite() { return this._stabilite; }

    setStabilite(val) {
        if (val > 100) {
            this._stabilite = 100;
        } else if (val <= 0) {
            this._stabilite = 0;
            throw new KuantumCokusuException(this._id);
        } else {
            this._stabilite = val;
        }
    }

    analizEt() {
        throw new Error("Bu metod override edilmelidir.");
    }

    durumBilgisi() {
        return `ID: ${this._id} | Stabilite: %${this._stabilite} | Tehlike: ${this._tehlikeSeviyesi}`;
    }
}


class VeriPaketi extends KuantumNesnesi {
    constructor(id) {
        super(id, 100, 1);
    }

    analizEt() {
        this.setStabilite(this.getStabilite() - 5);
        console.log(">> Veri içeriği okundu.");
    }
}

class KaranlikMadde extends KuantumNesnesi {
    constructor(id) {
        super(id, 100, 7);
    }

    analizEt() {
        this.setStabilite(this.getStabilite() - 15);
        console.log(">> Karanlık madde analiz edildi.");
    }

    
    acilDurumSogutmasi() {
        try {
            this.setStabilite(this.getStabilite() + 50);
            console.log(">> Soğutma başarılı.");
        } catch (e) {}
    }
}

class AntiMadde extends KuantumNesnesi {
    constructor(id) {
        super(id, 100, 10);
    }

    analizEt() {
        this.setStabilite(this.getStabilite() - 25);
        console.log(">> UYARI: Evrenin dokusu titriyor...");
    }

    acilDurumSogutmasi() {
        try {
            this.setStabilite(this.getStabilite() + 50);
            console.log(">> Anti-Madde stabilize edildi.");
        } catch (e) {}
    }
}


async function main() {
    let envanter = [];
    let sayac = 1;
    let sistemCalisiyor = true;

    console.log("--- KUANTUM AMBARI ---");

    while (sistemCalisiyor) {
        console.log("\n1. Yeni Nesne Ekle");
        console.log("2. Tüm Envanteri Listele");
        console.log("3. Nesneyi Analiz Et");
        console.log("4. Acil Durum Soğutması Yap");
        console.log("5. Çıkış");

        const secim = await soruSor("Seçiminiz: ");

        try {
            switch (secim) {
                case '1':
                    const rand = Math.floor(Math.random() * 3);
                    let yeniNesne;
                    const id = `NESNE-${1000 + sayac++}`;
                    if (rand === 0) yeniNesne = new VeriPaketi(id);

                    else if (rand === 1) yeniNesne = new KaranlikMadde(id);

                    else yeniNesne = new AntiMadde(id);
                    
                    envanter.push(yeniNesne);
                    console.log(`Eklendi: ${yeniNesne.constructor.name}`);
                    break;

                case '2':

                    if (envanter.length === 0) console.log("Depo boş.");
                    envanter.forEach(n => {
                        console.log(`${n.durumBilgisi()} [${n.constructor.name}]`);
                    });
                    break;

                case '3':

                    const aId = await soruSor("ID giriniz: ");
                    const aNesne = envanter.find(n => n.getId() === aId);
                    if (aNesne) {
                        aNesne.analizEt(); 
                    } else {
                        console.log("Nesne bulunamadı.");
                    }
                    break;

                case '4':

                    const sId = await soruSor("ID giriniz: ");
                    const sNesne = envanter.find(n => n.getId() === sId);
                    if (sNesne) {
                        
                        if (typeof sNesne.acilDurumSogutmasi === 'function') {
                            sNesne.acilDurumSogutmasi();

                        } else {
                            console.log("HATA: Bu nesne soğutulamaz!");
                        } }
                         else {

                        console.log("Nesne bulunamadı.");
                    }
                    break;

                case '5':
                    sistemCalisiyor = false;
                    console.log("Çıkış yapıldı.");
                    break;

                default:
                    console.log("Geçersiz seçim.");
            }
        } catch (e) {
            if (e.name === "KuantumCokusuException") {

                console.log("SİSTEM ÇÖKTÜ! TAHLİYE BAŞLATILIYOR...");
                console.log(e.message);
                sistemCalisiyor = false;
           
            } else {
                console.log("Beklenmedik bir hata oluştu: " + e);
            }
        }
    }
    rl.close();
}

main();