rem Для временного создания архива используется каталог /dev/shm
rem Забрать архив codec.tar с Арма
pscp -pw testing123 ./codec.tar root@192.168.1.29:/dev/shm/codec.tar >a.done

rem Создать командный файл a.tmp для распаковки архива codec.tar
echo tar -xf /dev/shm/codec.tar -C /;exit >a.tmp
rem Подключиться и выполнить a.tmp на кодеке, статистика при необходимости писать в a.done
plink -batch -m a.tmp -pw testing123 root@192.168.1.29 >>a.done


