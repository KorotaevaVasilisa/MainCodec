using System.Collections.Generic;

namespace TCPclient
{
    internal interface OnFileRemoteChanged
    {
        void onChanged(List<string> files);
        void onConnected(bool connected);
        void onCopyRemoteFile();
        void onUpdateProgressBar(int progress);
    }
}
