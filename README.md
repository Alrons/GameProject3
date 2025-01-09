# Перед работой в unity
Перед запуском проекта в unity нужно установить pgAdmin 17 версий и импортировать файл mydatabase_backup.sql
после чего нужно установить API.
>Ссылка на API https://github.com/дAlrons/GameAPI

Запустить проект в unity можно только после того, как будет запущена API и импортирована база данных.
По итогу должно получится так:

![result](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/asume.png)

# Анимация и эффекты
Под анимацию и эффекты я вынес отдельные файлы:

![allStates](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/allStates.png)

BuletState – файл привязанный к каждой пуле, в нем один метод который уничтожает пулю когда пуля доходит к цели. (по надобности могу перенести в этот файл и метод создания пули)

![buletState](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/buletState.png)

ItemSate – файл привязанный к каждому игровому предмету (items).  В этом файле есть переменная itemState которая меняет свое значение в зависимости от того, что происходит, ниже переменной написаны какие действия переменная может обозначать: 

![itemState](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/itemState.png)

TowerState – файл привязанный к каждой защите, как и BuketState имеет один метод который удаляет (разрушает) предмет. 

![towerState](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/towerState.png)

WaveState – файл, привязанный к волне. У него есть динамическая переменная, которая показывает 2 состояния и метод для уничтожения волны.

![WaveState](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/waveState.png)

PS. При необходимости расширю функционал.

# Как менять расположение волны
Чтобы поменять расположение волны нужно запустить проект WaveEditor. Этот проект находится в месте с проектом API. Дальше перейти по ссылке:

> https://localhost:7111/WaveEdit

После перейти во вкладку WavePosition и нажать на Edit.

![editWavePosition](https://github.com/Alrons/GameProject3/blob/ReadMe-file/Images/editWavePosition.png)

PS. Скорее всего при запуске проекта waveEditor вас перенесет на нужную страницу.
PS. Если ссылка не работает, то попробуйте изменить цифры в ссылке на те, что написаны в консоли, которая открылась при запуске проекта.
