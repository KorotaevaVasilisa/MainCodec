rem Удалить предыдущий архив
del  codec.tar
rem Для временного создания архива используется каталог /dev/shm
rem Создать командный файл a.tmp для создания архива codec.tar
echo tar -cf /dev/shm/codec.tar /temas/codec/*.*;exit >a.tmp
rem Подключиться и выполнить a.tmp на кодеке, статистика при необходимости писать в a.done
plink -batch -m a.tmp -pw testing123 root@192.168.1.29 >>a.done
rem Забрать архив codec.tar с кодека
pscp -pw testing123 root@192.168.1.29:/dev/shm/codec.tar ./ >>a.done
rem Создать командный файл a.tmp для удаления архива на кодеке (не обязательно)
echo rm /dev/shm/*.tar;exit >a.tmp
rem Удалить
plink -batch -m a.tmp -pw testing123 root@192.168.1.29


