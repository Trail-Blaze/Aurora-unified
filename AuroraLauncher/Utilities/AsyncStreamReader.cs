using System;
using System.IO;

class AsyncStreamReader
{
    #region Field Region

    StreamReader _reader;

    protected readonly byte[] _buffer;

    public event EventHandler<string> ValueRecieved;

    #endregion

    #region Property Region

    public bool Active { get; private set; }

    #endregion

    #region Constructor Region

    public AsyncStreamReader(StreamReader reader)
    {
        _reader = reader;

        _buffer = new byte[0x1000];

        Active = false;
    }

    #endregion

    #region Method Region

    public delegate void EventHandler<Args>(object sender, string value);

    protected void Begin()
    {
        if (Active)
            _reader.BaseStream.BeginRead(_buffer, 0, _buffer.Length, new AsyncCallback(Read), null);
    }

    public void Start()
    {
        if (!Active)
        {
            Active = true;

            Begin();
        }
    }

    public void Stop() => Active = false;

    void Read(IAsyncResult result)
    {
        if (_reader != null)
        {
            int count = _reader.BaseStream.EndRead(result);
            string value = null;

            if (count > 0) value = _reader.CurrentEncoding.GetString(_buffer, 0, count);
            else Active = false;

            ValueRecieved?.Invoke(this, value);

            Begin();
        }
    }

    #endregion
}
