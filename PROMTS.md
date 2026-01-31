## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı | X |
| Kullanılan araçlar | ChatGPT / Claude / Copilot |
| En çok yardım alınan konular | [liste] |
| Tahmini LLM ile kazanılan süre | X saat |

---

## Prompt 1: [Proje ve LLM'in projedeki rol tanımı]

**Araç:** [Gemini 3 Flash]
**Tarih/Saat:** 2026-01-31 18:54

**Prompt:**
```
[I will make a Case Study in this Unity project for a Game Studio. They wanna "World Interaction System.
A moduler Interaction system in Unity." from me. 

So your role is to be a great Senior Game developer and create the system modular, optimized, clean coded. We need to build this system like AAA studios.]
```

**Alınan Cevap (Özet):**
```
[Tüm scriptleri tek prompt ile yazmaya çalıştı :D. Sanırım daha açıklayıcı olmam lazımdı.]
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [X] Reddettim (kullanmadım)

**Açıklama:**
> [Sistem kurgusunu kendim kuracağım için reddettim.]

---

## Prompt 2: [Konu Başlığı]

**Araç:** [Gemini 3 Pro Preview with Google AI Studio]
**Tarih/Saat:** 19:12

**Prompt:**
```
[Now we need to create a modular Input System. This system needs to be:

-Use New Input System
-Event based and Centralized
-ScriptableObject based
-Its like a Input Data Center, it broadcasts the Input datas and the other scripts can listen it and get the datas to use their own purposes]
```

**Alınan Cevap (Özet):**
```
[Kullanmam gereken gereksinimlerden bahsetti ve scripti yazdı.]
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [X] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> [Event mantığını ve methodları güzel kurdu, revize edip kullandım. Input assetini configure ederken zaman kaybettim çünkü assetten değil de, scriptin kendisinden bağlayacakmışız inputları. Bunu çözmem biraz zaman aldı.]

---

## Prompt 2: [Konu Başlığı]

**Araç:** [Gemini 3 Pro Preview with Google AI Studio]
**Tarih/Saat:** 20:26

**Prompt:**
```
[Şimdi senden kapsamlı bir interaction sistemi istemekteyim. Önce planlayalım ve sonra uygulamaya geçelim. İstediğim Interaction Systemın temelleri şunlar:
``
Beklenen Yapı:
IInteractable interface
InteractionDetector (raycast veya trigger-based)
Interaction range kontrolü
Single interaction point (aynı anda tek nesne ile etkileşim)
code
Code
**Teknik Detaylar:**
- Oyuncu belirli bir mesafeden nesnelerle etkileşime geçebilmeli
- Birden fazla interactable aynı range'de ise en yakın olanı seçilmeli
- Etkileşim input'u configurable olmalı (Inspector'dan değiştirilebilir)

### 2. Interaction Types (En az 3 tür)

| Tür | Açıklama | Örnek Kullanım |
|-----|----------|----------------|
| **Instant** | Tek tuş basımı ile anında | Pickup item, button press |
| **Hold** | Basılı tutma gerektiren | Chest açma, kapı kilidi kırma |
| **Toggle** | Açık/kapalı durumlar | Light switch, door |

Her interaction type için base class veya interface oluşturulmalıdır. 

Şimdi bu sistemi planlamaya başlayalım!]
```

**Alınan Cevap (Özet):**
```
[Mimari plan yapmaya başladı. Plana göre: 
Temel katman: IInteractable. Her şeyin atası olacak. Player karşımızdaki objenin ne olduğunu bilmeyecek. Sadece interactable olduğunu bilecek ve interact sinyali gönderecek o kadar.
Logic Katmanı: InteractionDetector: Bu script etraftaki interactableları tarayacak ve en uygun interactableı bulacak.
World Katmanı: Objeler etrafata bulunacak ve onlarla E ile interact edeceğiz.
]
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [X] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> [Mimari planlama emri verdiğim için güzel bir plan sundu. Bunu uygulaması için onay verdim ve kodları entegre ederek kullandım.]

---

## Notlar

- Her önemli LLM etkileşimini kaydedin
- Copy-paste değil, anlayarak kullandığınızı gösterin
- LLM'in hatalı cevap verdiği durumları da belirtin
- Dürüst olun - LLM kullanımı teşvik edilmektedir

---

*Bu şablon Ludu Arts Unity Intern Case için hazırlanmıştır.*
