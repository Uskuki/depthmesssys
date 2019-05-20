using INFRASTRUCTURE.Context.User;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace INFRASTRUCTURE
{
    public abstract class CustomHub : Hub, IDisposable
    {
        private bool _disposed;

        private static UserContext _userContext;
        
        public CustomHub()
        {
        }

        public UserContext UserContext
        {
            get
            {
                CheckDisposed();
                return _userContext;
            }
            set
            {
                CheckDisposed();
                _userContext = value;
            }
        }

        public virtual Task BackConnectedAsync(string username)
        {
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Dispose(true);
            _disposed = true;
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
