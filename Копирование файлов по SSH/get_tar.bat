rem ������� ���������� �����
del  codec.tar
rem ��� ���������� �������� ������ ������������ ������� /dev/shm
rem ������� ��������� ���� a.tmp ��� �������� ������ codec.tar
echo tar -cf /dev/shm/codec.tar /temas/codec/*.*;exit >a.tmp
rem ������������ � ��������� a.tmp �� ������, ���������� ��� ������������� ������ � a.done
plink -batch -m a.tmp -pw testing123 root@192.168.1.29 >>a.done
rem ������� ����� codec.tar � ������
pscp -pw testing123 root@192.168.1.29:/dev/shm/codec.tar ./ >>a.done
rem ������� ��������� ���� a.tmp ��� �������� ������ �� ������ (�� �����������)
echo rm /dev/shm/*.tar;exit >a.tmp
rem �������
plink -batch -m a.tmp -pw testing123 root@192.168.1.29


