# WarehouseApp

WarehouseApp to  aplikacja webowa stworzona w technologii ASP.NET Core 8, umożliwiająca zarządzanie asortymentem produktów  w magazynie, odbieraniem zamówień oraz zarządzaniem zamówieniami.



### Funkcjonalności

#### Użytkownicy niezalogowani:

- Przeglądanie produktów – Użytkownicy mogą przeglądać listę dostępnych produktów w magazynie.
- Podgląd szczegółowych informacji – Użytkownicy mogą zobaczyć szczegółowe informacje na temat wybranego produktu, w tym opis, cenę i dostępność.

#### Użytkownicy zalogowani:

- Tworzenie zamówień – Użytkownicy mogą składać zamówienia.
- Przeglądanie swoich zamówień – Dostęp do historii złożonych zamówień wraz z informacją o zamówionych produktach, ilości oraz łącznej cenie.
- Podgląd szczegółów zamówienia – Użytkownicy mogą sprawdzić szczegóły złożonych zamówień, w tym status („Pending”, „Completed”, „Cancelled”).

#### Dla administratorów:

- Zarządzanie produktami – Administratorzy mogą dodawać, edytować i usuwać produkty.
- Zarządzanie kategoriami – Możliwość organizowania produktów w kategorie, co ułatwia ich przeglądanie przez użytkowników.
- Przeglądanie zamówień – Administratorzy mogą przeglądać listę wszystkich złożonych zamówień oraz ich szczegóły.
- Zarządaznie zamówieniami - Administratorzy mogą zmieniać status zamówienia.

### Jak uruchomić projekt

#### Wymagania wstępne:

- Zainstalowany .NET SDK 8
- Dostęp do Microsoft SQL Server
- Visual Studio 2022 lub inny kompatybilny edytor
- Instrukcja instalacji:
Pobierz projekt:



#### Otwórz terminal i użyj poniższego polecenia, aby sklonować repozytorium:

bash

Skopiuj kod
git clone https://github.com/yourusername/WarehouseApp.git

cd WarehouseApp

### Pobierz potrzebne biblioteki:

#### Wejdź w Zarządzanie pakietami NuGet w Visual Studio i dodaj następujące pakiety:

- Microsoft.AspNetCore.Identity.EntityFrameworkCore

- Microsoft.AspNetCore.Identity.UI

- Microsoft.EntityFrameworkCore.SqlServer

- Microsoft.EntityFrameworkCore.Tools

- Microsodt.EnittyFrameworkCore.Design

### Skonfiguruj bazę danych:

Edytuj plik appsettings.json i zaktualizuj sekcję ConnectionStrings, aby pasowała do Twoich ustawień bazy danych:

json
Skopiuj kod
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=WarehouseApp;Trusted_Connection=True;MultipleActiveResultSets=true"
}

### Zainicjuj bazę danych:

#### Otwórz Menedżer pakietów NuGet w Visual Studio i wykonaj poniższe polecenia:

bash
Skopiuj kod
ADD-MIGRATION Initial
UPDATE-DATABASE
Uruchom aplikację:

 W Visual Studio wybierz Run lub Ctrl+F5, aby uruchomić aplikację.

# Korzystanie z aplikacji

## Role użytkowników:

### Administrator:

#### Login: admin@admin.com

#### Hasło: ZAQ!wsx

### Zwykły użytkownik:

W celu uzyskania dostępu, użytkownik może założyć nowe konto poprzez formularz rejestracji dostępny na stronie głównej.

### Dostępne funkcje:

#### Administratorzy mają pełny dostęp do wszystkich funkcji zarządzania:

- Możliwość dodawania, edycji i usuwania produktów.
- Tworzenie kategori produktów oraz ich edycji
- Przeglądanie i zarządzanie zamówieniami.

#### Zwykli użytkownicy mogą:

- Przeglądać produkty i dokonywać zamówień.
- Złożyć zamówienie oraz przeglądać swoje zamówienia.

# Podsumowanie

WarehouseApp to nowoczesna aplikacja umożliwiająca sprawne zarządzanie magazynem oraz zamówieniami. Użytkownicy mogą wygodnie składać zamówienia, natomiast administratorzy mają pełną kontrolę nad produktami i stanami magazynowymi. WarehouseApp wyróżnia się intuicyjnym interfejsem, który usprawnia obsługę i pozwala na efektywne zarządzanie całym procesem magazynowym oraz sprzedażowym.
