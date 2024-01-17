rem Создать командный файл a.tmp для удаления на кодеке (обязательно)
echo mv /temas/codec/codec /temas/codec/codec.res;exit >a.tmp
rem Переименовать в .res
plink -batch -m a.tmp -pw testing123 root@192.168.1.29
rem Передать codec на кодека
pscp -pw testing123 ./codec root@192.168.1.29:/temas/codec/codec >a.done


