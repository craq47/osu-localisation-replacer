# osu-stable! Localisation Auto-Replacer 1.3 🧐
# Outdated!
🇬🇧 Tired of reinstalling localisation every time you update osu!? Look no further!

🇺🇦 Набридло перевстановлювати локалізацію щоразу після оновлення osu!? Тоді вам сюди!

# How it works? ⚙️
🇬🇧 To use the utility, place the executable file in the game folder (where the game launch file is located) and launch the game using this executable file.
The utility compares the original localization files with those in the 'custom' folder. If the file size of the version in the 'custom' folder is larger than the original, the utility replaces it.
You can create a shortcut and use it instead of the regular osu! shortcut.

🇺🇦 Для роботи треба помістити виконуваний файл у теку з грою (де знаходиться файл запуску гри) і запускати гру за допомогою цього виконуючого файлу.
Утиліта оцінює оригінальні файли локалізації та файли у теці "custom", якщо розмір версії файлу у теці "custom" більший за оригінал, то утиліта замінює його.
Ви можете створити Ярлик і просто використовувати його замість звичайного Ярлика osu!.

# Work intricacies! 🔬
🇬🇧 In the **1.3** update, we improved the management of program parameters through object arguments in the shortcut properties.
To set a property correctly, you should put two dashes after the path to the object, specify the necessary parameters, put an equal sign, and specify their values without quotes.

 _**For example:** D:\Games\osu!\osu!loc.exe **--update-interval=100** **--updates-period=2** **--disable-logging=true**_

 All existing parameters:
 - **update-interval** — the interval between checks, in milliseconds. The default value is **100**.
 - **updates-period** — the time in seconds during which the checks will be performed. The default value is **2**.
 - **disable-logging** — disables, if the value is 'true' or enables, if the value is 'false', logging of debugging in the 'Logs' folder. The default value is **false**.

🇺🇦 У оновленні **1.3** було вдосконалено керування параметрами програми через аргументи об'єкта у властивостях ярлика.
Щоби правильно задати властивості, слід поставити дві риски, вказати після шляху до об'єкта необхідні параметри та поставити дорівнює та вказати їх значення, без кавичок.

 _**Наприклад:** D:\Games\osu!\osu!loc.exe **--update-interval=100** **--updates-period=2** **--disable-logging=true**_

 Усі існуючі параметри:
 - **update-interval** — інтервал між перевірками, у мілісекундах. За замовчуванням — **100**.
 - **updates-period** — час у секундах, протягом якого будуть виконуватися перевірки. За замовчуванням — **2**.
 - **disable-logging** — вимикає, якщо значення "true" або вмикає — "false" ведення журналу відлагодження у теці "Logs". За замовчуванням — **false**.
