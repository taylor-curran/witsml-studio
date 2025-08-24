using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Security;

namespace Energistics.DataAccess
{
    public enum AuthenticationTypes
    {
        Basic,
        Bearer
    }

    public enum CompressionMethods
    {
        None,
        Gzip,
        Deflate
    }

    public enum WebSocketType
    {
        Native,
        SuperSocket
    }

    [DataContract]
    public class Connection : INotifyPropertyChanged
    {
        public Connection()
        {
            SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            IsAuthenticationBasic = true;
            PreAuthenticate = true;
            ProxyPort = 80;
        }

        private string? _name;
        private string? _uri;
        private string? _username;
        private string? _clientId;
        private string? _proxyHost;
        private string? _proxyUsername;
        private string? _jsonWebToken;
        private AuthenticationTypes _authenticationType;
        private SecurityProtocolType _securityProtocol;
        private WebSocketType _webSocketType;
        private CompressionMethods _soapRequestCompressionMethod;
        private int _proxyPort;
        private bool _proxyUseDefaultCredentials;
        private bool _acceptInvalidCertificates;
        private bool _preAuthenticate;
        private bool _isAuthenticationBasic;
        private bool _isAuthenticationBearer;
        private bool _soapAcceptCompressedResponses;

        [DataMember]
        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange(nameof(Name));
                }
            }
        }

        [DataMember]
        public string? Uri
        {
            get => _uri;
            set
            {
                if (_uri != value)
                {
                    _uri = value;
                    NotifyOfPropertyChange(nameof(Uri));
                }
            }
        }

        [DataMember]
        public string? Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    NotifyOfPropertyChange(nameof(Username));
                }
            }
        }

        [DataMember]
        public string? Password { get; set; }

        public SecureString? SecurePassword { get; set; }

        [DataMember]
        public string? ClientId
        {
            get => _clientId;
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                    NotifyOfPropertyChange(nameof(ClientId));
                }
            }
        }

        [DataMember]
        public AuthenticationTypes AuthenticationType
        {
            get => _authenticationType;
            set
            {
                if (_authenticationType != value)
                {
                    _authenticationType = value;
                    NotifyOfPropertyChange(nameof(AuthenticationType));
                }
            }
        }

        [DataMember]
        public SecurityProtocolType SecurityProtocol
        {
            get => _securityProtocol;
            set
            {
                if (_securityProtocol != value)
                {
                    _securityProtocol = value;
                    NotifyOfPropertyChange(nameof(SecurityProtocol));
                }
            }
        }

        [DataMember]
        public string? ProxyHost
        {
            get => _proxyHost;
            set
            {
                if (_proxyHost != value)
                {
                    _proxyHost = value;
                    NotifyOfPropertyChange(nameof(ProxyHost));
                }
            }
        }

        [DataMember]
        public int ProxyPort
        {
            get => _proxyPort;
            set
            {
                if (_proxyPort != value)
                {
                    _proxyPort = value;
                    NotifyOfPropertyChange(nameof(ProxyPort));
                }
            }
        }

        [DataMember]
        public string? ProxyUsername
        {
            get => _proxyUsername;
            set
            {
                if (_proxyUsername != value)
                {
                    _proxyUsername = value;
                    NotifyOfPropertyChange(nameof(ProxyUsername));
                }
            }
        }

        [DataMember]
        public string? ProxyPassword { get; set; }

        public SecureString? SecureProxyPassword { get; set; }

        [DataMember]
        public bool ProxyUseDefaultCredentials
        {
            get => _proxyUseDefaultCredentials;
            set
            {
                if (_proxyUseDefaultCredentials != value)
                {
                    _proxyUseDefaultCredentials = value;
                    NotifyOfPropertyChange(nameof(ProxyUseDefaultCredentials));
                }
            }
        }

        [DataMember]
        public string? JsonWebToken
        {
            get => _jsonWebToken;
            set
            {
                if (_jsonWebToken != value)
                {
                    _jsonWebToken = value;
                    NotifyOfPropertyChange(nameof(JsonWebToken));
                }
            }
        }

        [DataMember]
        public WebSocketType WebSocketType
        {
            get => _webSocketType;
            set
            {
                if (_webSocketType != value)
                {
                    _webSocketType = value;
                    NotifyOfPropertyChange(nameof(WebSocketType));
                }
            }
        }

        [DataMember]
        public bool AcceptInvalidCertificates
        {
            get => _acceptInvalidCertificates;
            set
            {
                if (_acceptInvalidCertificates != value)
                {
                    _acceptInvalidCertificates = value;
                    NotifyOfPropertyChange(nameof(AcceptInvalidCertificates));
                }
            }
        }

        [DataMember]
        public bool PreAuthenticate
        {
            get => _preAuthenticate;
            set
            {
                if (_preAuthenticate != value)
                {
                    _preAuthenticate = value;
                    NotifyOfPropertyChange(nameof(PreAuthenticate));
                }
            }
        }

        [DataMember]
        public bool IsAuthenticationBasic
        {
            get => _isAuthenticationBasic;
            set
            {
                if (_isAuthenticationBasic != value)
                {
                    _isAuthenticationBasic = value;
                    IsAuthenticationBearer = !value;
                    NotifyOfPropertyChange(nameof(IsAuthenticationBasic));
                }
            }
        }

        [DataMember]
        public bool IsAuthenticationBearer
        {
            get => _isAuthenticationBearer;
            set
            {
                if (_isAuthenticationBearer != value)
                {
                    _isAuthenticationBearer = value;
                    IsAuthenticationBasic = !value;
                    NotifyOfPropertyChange(nameof(IsAuthenticationBearer));
                }
            }
        }

        [DataMember]
        public CompressionMethods SoapRequestCompressionMethod
        {
            get => _soapRequestCompressionMethod;
            set
            {
                if (_soapRequestCompressionMethod != value)
                {
                    _soapRequestCompressionMethod = value;
                    NotifyOfPropertyChange(nameof(SoapRequestCompressionMethod));
                }
            }
        }

        [DataMember]
        public bool SoapAcceptCompressedResponses
        {
            get => _soapAcceptCompressedResponses;
            set
            {
                if (_soapAcceptCompressedResponses != value)
                {
                    _soapAcceptCompressedResponses = value;
                    NotifyOfPropertyChange(nameof(SoapAcceptCompressedResponses));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyOfPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WITSMLWebServiceConnection CreateProxy(WMLSVersion version)
        {
            return new WITSMLWebServiceConnection(this, version);
        }
        
        public WITSMLWebServiceConnection CreateProxy(string version)
        {
            var versionEnum = version switch
            {
                "1.3.1.1" => WMLSVersion.WITSML131,
                "1.4.1.1" => WMLSVersion.WITSML141,
                _ => WMLSVersion.WITSML141
            };
            return new WITSMLWebServiceConnection(this, versionEnum);
        }

        public override string ToString()
        {
            return $"Uri: {Uri}; Username: {Username}; AuthenticationType: {AuthenticationType}";
        }
    }
}
