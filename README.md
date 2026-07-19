# ChickEscape

Mutfağa sızan bir civciv olarak yumurtaları toplayıp kediden kaçtığınız 3D aksiyon-macera oyunu.

## Hızlı Başlangıç

1. [Unity Hub](https://unity.com/download) üzerinden **Unity 6000.5.2f1** sürümünü kurun.
2. Depoyu klonlayın ve Unity Hub ile `ChickEscape/` klasörünü proje olarak açın.
3. İlk açılışta bağımlılıkların indirilmesini bekleyin.
4. `Assets/_GameAssets/Scenes/MenuScene.unity` sahnesini açıp **Play** tuşuna basın.

> Unity projesi depo kökündeki `ChickEscape/` klasöründedir.

## Oynanış

Oyun bir mutfak/restoran ortamında geçer. Açılış sinematiğinin ardından civciv olarak kontrolü ele alırsınız.

**Amaç:** Haritadaki **5 yumurtayı** toplamak.

**Kaybetme koşulları:**
- Kedi sizi yakalarsa
- Canınız (3 kalp) biterse — ateş gibi tehlikeler hasar verir

**Kazanma koşulu:** Tüm yumurtalar toplandığında oyun biter ve kazanma ekranı gösterilir.

### Katmanlar ve kedi davranışı

Kedi, NavMesh üzerinde devriye gezer. Oyuncu **zemin (Ground)** katmanındayken kedi yalnızca devriye modunda kalır; **üst kat (Floor)** katmanına çıktığınızda sizi kovalamaya başlar. Rotayı buna göre planlayın.

### Toplanabilirler

| Nesne | Etki |
|-------|------|
| **Yumurta** | Ana hedef; 5 adet toplanınca kazanırsınız |
| **Altın Buğday** | Geçici hız artışı |
| **Kutsal Buğday** | Geçici zıplama gücü artışı |
| **Çürük Buğday** | Geçici yavaşlama |

### Tehlikeler ve güçlendirmeler

- **Ateş:** Temas halinde 1 hasar verir ve geri iter.
- **Spatula:** Üzerine basıldığında ekstra zıplama kuvveti sağlar.

## Kontroller

| Tuş | Eylem |
|-----|-------|
| `W` `A` `S` `D` | Hareket |
| `Space` | Zıpla |
| Fare | Üçüncü şahıs kamera yönü (hareket yönünü belirler) |

Oyun içi **Ayarlar** menüsünden müzik ve ses efektlerini açıp kapatabilir, oyuna devam edebilir veya ana menüye dönebilirsiniz.

## Proje Yapısı

```
ChickEscape/
├── Assets/
│   ├── _GameAssets/
│   │   ├── Animations/      # Oyuncu, kedi, yumurta, buğday animasyonları
│   │   ├── Prefabs/         # Toplanabilirler, ortam dekorları, ses prefab'ları
│   │   ├── Scenes/
│   │   │   ├── MenuScene.unity
│   │   │   └── GameScene.unity
│   │   ├── Scripts/
│   │   │   ├── Audio/           # AudioManager, BackgroundMusic
│   │   │   ├── Boostables/      # Spatula gibi güçlendirmeler
│   │   │   ├── Collectibles/    # Yumurta ve buğday toplanabilirleri
│   │   │   ├── Damageables/     # Ateş gibi hasar kaynakları
│   │   │   ├── Enums/           # GameState, PlayerState, CatState
│   │   │   ├── GamePlay/        # Oyuncu, kedi, kamera kontrolcüleri
│   │   │   ├── Helpers/         # Consts, CameraShake, ScriptableObject'ler
│   │   │   ├── Managers/        # GameManager, HealthManager, TimeLineManager
│   │   │   └── UI/              # Menü, timer, sağlık, kazan/kaybet ekranları
│   │   ├── Sounds/
│   │   ├── Sprites/
│   │   └── 3rdParty/            # Üçüncü taraf asset paketleri
│   ├── Plugins/Demigiant/       # DOTween
│   └── Settings/                # URP render ayarları
├── Packages/manifest.json
└── ProjectSettings/
```

## Teknoloji Yığını

| Bileşen | Sürüm / Açıklama |
|---------|------------------|
| Unity | 6000.5.2f1 |
| Render Pipeline | Universal Render Pipeline (URP) |
| Cinemachine | 3.1.7 |
| AI Navigation | 2.0.13 |
| Input System | 1.19.0 |
| Timeline | 1.8.12 |
| DOTween | Animasyon ve UI geçişleri |
| TextMesh Pro | UI metinleri |

## Mimari Özet

Oyun, singleton tabanlı yöneticiler ve olay (event) sistemi üzerine kuruludur:

- **GameManager** — Oyun durumu (`CutScene`, `Play`, `Pause`, `Resume`, `GameOver`), yumurta sayacı ve kazan/kaybet akışı
- **HealthManager** — Oyuncu canı (varsayılan 3)
- **TimeLineManager** — Açılış sinematiği; bittiğinde oyun `Play` durumuna geçer
- **CatController** — NavMesh devriyesi ve kovalamaca AI
- **PlayerController** — Fizik tabanlı hareket, zıplama ve kayma
- **PlayerInteractionController** — Toplanabilir, güçlendirme ve hasar etkileşimleri

Toplanabilirler `ICollectible`, güçlendirmeler `IBoostables`, hasar kaynakları `IDamageable` arayüzleri üzerinden çalışır.

## Üçüncü Taraf Asset'ler

Proje aşağıdaki harici paketleri kullanır (lisansları ilgili klasörlerde belirtilmiştir):

- **KayKit Restaurant** — Mutfak modelleri
- **Tiny Treats** — Ek ortam modelleri
- **Fantasy Skybox FREE** — Gökyüzü
- **VFXPACK FIRE (Wallcoeur)** — Ateş efektleri
- **Nearmint Studios — Mask Transitions** — Sahne geçiş animasyonları
- **DOTween (Demigiant)** — Tween animasyonları
- **TextMesh Pro** — Unity varsayılan UI font sistemi

## Geliştirme

### Build alma

1. **File → Build Settings** menüsünü açın.
2. `MenuScene` ve `GameScene` sahnelerinin build listesinde olduğundan emin olun.
3. Hedef platformu seçip **Build** veya **Build And Run** ile derleyin.

### Sahne akışı

```
MenuScene → (Play) → GameScene → CutScene → Play → GameOver → Win/Lose popup
```

Ana menüden çıkış `Application.Quit()` ile yapılır; editörde bu çağrı etkisizdir.

## Lisans

Bu proje için henüz bir lisans dosyası tanımlanmamıştır. Üçüncü taraf asset'ler kendi lisans koşullarına tabidir.
