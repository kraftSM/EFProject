# Д/З по Юниту  EFProject
## Д/З по Юниту  EFProject
- Вывод данных о записи в консоль использует метод.ToString классов Book, User
- Запрос данных (String, Int) через консоль использует класс UserIO. Вывод в консоль из репозитриев тоже стоило бы перенести туда же, но это в "следующей жизни", пока иногда есть прямой вывод
- NavMenu реализутся простейший интерфейс-"посредник" между пользователем и DataRegistry (по текстовому и/или числовому выобору действий/запросов)
- Репозиториях Методы Select... вызывают LINQ-фильтры Get... ? запрашивая доп. данные у пользователя через UserIO, динамически создавая контекст подключения.
