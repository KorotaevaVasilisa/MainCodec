rem ������� ��������� ���� a.tmp ��� �������� �� ������ (�����������)
echo mv /temas/codec/codec /temas/codec/codec.res;exit >a.tmp
rem ������������� � .res
plink -batch -m a.tmp -pw testing123 root@192.168.1.29
rem �������� codec �� ������
pscp -pw testing123 ./codec root@192.168.1.29:/temas/codec/codec >a.done


