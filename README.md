# Drinks

A C# console application for browsing cocktail recipes via a public API, with favourites management and a drink view counter backed by a local SQLite database.


## Features

- **Browse by Category** — Fetch all drink categories from the API and browse drinks within each one
- **Drink Details** — View full recipe information including ingredients, measures, instructions, glass type and an inline image rendered in the terminal
- **Favourites** — Save drinks to a local favourites list and remove them at any time
- **View Counter** — Automatically tracks how many times you have looked up each drink, sorted by most viewed


## Getting Started

### Prerequisites

- .NET 8 SDK
- An internet connection (the app calls [TheCocktailDB](https://www.thecocktaildb.com/) public API)

### Installation

1. **Clone the repository**
   ```bash
   git clone <your-repository-url>
   cd StressedBread.Drinks
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

   On first launch the app will automatically create the SQLite database file and all required tables. Subsequent launches safely skip this step.


## How to Use

### 1. Browsing Drink Categories

- Select **View Drink Categories** from the main menu
- A list of all available categories is fetched from the API
- Use the arrow keys to select a category and press Enter

### 2. Browsing Drinks in a Category

- After selecting a category, a list of all drinks in that category is displayed
- Select a drink to view its full details
- Select **Back** to return to the category list

### 3. Viewing Drink Details

- The detail screen shows the drink name, category, glass type, whether it is alcoholic, full instructions, all ingredients with measures, and a thumbnail image rendered inline
- From here you can either **Add to Favourites** or **Exit** back to the main menu

### 4. Managing Favourites

- Select **View Favourite Drinks** from the main menu
- All saved favourites are displayed in a table with their ID and name
- Enter the **ID** of a drink to remove it from favourites
- Enter **0** to return to the main menu without making changes

### 5. View Counter

- Select **View Drink View Counter** from the main menu
- A table shows every drink you have ever opened, sorted by the number of times viewed
- The counter increments automatically each time you open a drink's detail screen


## Technologies Used

- **Framework**: .NET 8
- **Language**: C# 12
- **Database**: SQLite with Dapper
- **API**: [TheCocktailDB](https://www.thecocktaildb.com/) free public API
- **UI**: Spectre.Console


## Configuration

The app reads its settings from `appsettings.json`:

| Key | Purpose |
|---|---|
| `ConnectionStrings:DefaultConnection` | Path to the local SQLite database file |
| `ApiSettings:BaseUrl` | Base URL for TheCocktailDB API |

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Drinks.db;"
  },
  "ApiSettings": {
    "BaseUrl": "https://www.thecocktaildb.com/api/json/v1/1/"
  }
}
```

The SQLite database file is created automatically in the working directory on first run. No manual database setup is required.