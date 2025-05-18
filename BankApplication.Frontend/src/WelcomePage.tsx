import styles from "./styles/WelcomePage.module.css";
import NavBar from "./NavBar";
import Footer from "./Footer";

function WelcomePage() {
  return (
    <>
      <NavBar />

      <header className={styles.header}>
        <h1 className={styles.headerTitle}>Pocket bank - dowiedz się więcej</h1>
        <img
          src="https://cdn-icons-png.flaticon.com/512/190/190411.png"
          alt="Money icon"
          className={styles.headerImage}
        />
      </header>

      <main className={styles.main}>
        <section className={styles.section}>
          <h2 className={styles.sectionTitle}>
            PocketBank - Twój bank zawsze pod ręką
          </h2>
          <p>
            W PocketBank łączymy nowoczesne technologie z najwyższym poziomem
            bezpieczeństwa, aby zapewnić Ci pełną kontrolę nad Twoimi finansami.
          </p>
          <p>
            Za pomocą naszego serwisu internetowego możesz łatwo i szybko
            zarządzać swoimi rachunkami, wykonywać przelewy, sprawdzać historię
            transakcji oraz korzystać z wielu dodatkowych usług bez wychodzenia
            z domu.
          </p>
          <p>
            Prosty i intuicyjny interfejs sprawia, że bankowość staje się
            przyjemnością — niezależnie czy korzystasz z komputera, tabletu czy
            smartfona.
          </p>
          <p>
            Z PocketBank możesz mieć pewność, że Twoje pieniądze są zawsze
            bezpieczne, a Twoje dane chronione na najwyższym poziomie.
          </p>
          <img
            src="https://cdn-icons-png.flaticon.com/512/1170/1170576.png"
            alt="Mobile banking"
            className={styles.sectionImageLarge}
          />
        </section>

        <section className={styles.section} style={{ marginBottom: "30px" }}>
          <h3>Jak działamy?</h3>
          <p>
            Nasza platforma działa 24/7, dzięki czemu masz stały dostęp do
            swoich środków i usług bankowych, gdziekolwiek jesteś.
            Wykorzystujemy najnowsze technologie, by zapewnić szybkość i
            bezpieczeństwo każdej transakcji.
          </p>
        </section>

        <section className={styles.section} style={{ marginBottom: "30px" }}>
          <h3>Dla kogo jest PocketBank?</h3>
          <p>
            Zapraszamy wszystkich, którzy cenią sobie wygodę i nowoczesne
            rozwiązania. Niezależnie, czy jesteś osobą prywatną, przedsiębiorcą,
            czy freelancerem – PocketBank dostosuje się do Twoich potrzeb.
          </p>
        </section>

        <section className={styles.section} style={{ marginBottom: "30px" }}>
          <h3>Dlaczego warto z nami współpracować?</h3>
          <p>
            Oferujemy indywidualne podejście, przejrzyste warunki oraz wsparcie
            na każdym etapie korzystania z usług. Naszym celem jest uproszczenie
            Twojej codziennej bankowości i bezpieczeństwo Twoich finansów.
          </p>
        </section>

        <section className={styles.section} style={{ marginBottom: "30px" }}>
          <h3>Bezpieczeństwo na pierwszym miejscu</h3>
          <p>
            Stosujemy zaawansowane mechanizmy zabezpieczeń, takie jak
            wielopoziomowa autoryzacja i szyfrowanie danych, by chronić Twoje
            środki i prywatność.
          </p>
          <img
            src="https://cdn-icons-png.flaticon.com/512/3064/3064197.png"
            alt="Security icon"
            className={styles.sectionImageSmall}
          />
        </section>
      </main>

      <Footer />
    </>
  );
}

export default WelcomePage;
