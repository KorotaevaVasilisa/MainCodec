rem ��� ���������� �������� ������ ������������ ������� /dev/shm
rem ������� ����� codec.tar � ����
pscp -pw testing123 ./codec.tar root@192.168.1.29:/dev/shm/codec.tar >a.done

rem ������� ��������� ���� a.tmp ��� ���������� ������ codec.tar
echo tar -xf /dev/shm/codec.tar -C /;exit >a.tmp
rem ������������ � ��������� a.tmp �� ������, ���������� ��� ������������� ������ � a.done
plink -batch -m a.tmp -pw testing123 root@192.168.1.29 >>a.done


