using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Security;

namespace WitsmlFramework;

[DataContract]
public class Connection : INotifyPropertyChanged
{
    public Connection()
    {
        SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        IsAuthenticationBasic = true;
        PreAuthenticate = true;
        ProxyPort = 80;
        RedirectPort = 9005;
    }

    private AuthenticationTypes _authenticationType;

    [DataMember]
    public AuthenticationTypes AuthenticationType
    {
        get { return _authenticationType; }
        set
        {
            if (_authenticationType != value)
            {
                _authenticationType = value;
                NotifyOfPropertyChange(nameof(AuthenticationType));
            }
        }
    }

    private SecurityProtocolType _securityProtocol;

    [DataMember]
    public SecurityProtocolType SecurityProtocol
    {
        get { return _securityProtocol; }
        set
        {
            if (_securityProtocol != value)
            {
                _securityProtocol = value;
                NotifyOfPropertyChange(nameof(SecurityProtocol));
            }
        }
    }

    private string _name;

    [DataMember]
    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.Equals(_name, value))
            {
                _name = value;
                NotifyOfPropertyChange(nameof(Name));
            }
        }
    }

    private string _uri;

    [DataMember]
    public string Uri
    {
        get { return _uri; }
        set
        {
            if (!string.Equals(_uri, value))
            {
                _uri = value;
                NotifyOfPropertyChange(nameof(Uri));
            }
        }
    }

    private string _username;

    [DataMember]
    public string Username
    {
        get { return _username; }
        set
        {
            if (!string.Equals(_username, value))
            {
                _username = value;
                NotifyOfPropertyChange(nameof(Username));
            }
        }
    }

    [DataMember]
    public string Password { get; set; }

    public SecureString SecurePassword { get; set; }

    private bool _acceptInvalidCertificates;

    [DataMember]
    public bool AcceptInvalidCertificates
    {
        get { return _acceptInvalidCertificates; }
        set
        {
            if (_acceptInvalidCertificates != value)
            {
                _acceptInvalidCertificates = value;
                NotifyOfPropertyChange(nameof(AcceptInvalidCertificates));
            }
        }
    }

    private bool _preAuthenticate;

    [DataMember]
    public bool PreAuthenticate
    {
        get { return _preAuthenticate; }
        set
        {
            if (_preAuthenticate != value)
            {
                _preAuthenticate = value;
                NotifyOfPropertyChange(nameof(PreAuthenticate));
            }
        }
    }

    private bool _isAuthenticationBasic;

    [DataMember]
    public bool IsAuthenticationBasic
    {
        get { return _isAuthenticationBasic; }
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
    
    private bool _isAuthenticationBearer;

    [DataMember]
    public bool IsAuthenticationBearer
    {
        get { return _isAuthenticationBearer; }
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

    private CompressionMethods _soapRequestCompressionMethod;

    [DataMember]
    public CompressionMethods SoapRequestCompressionMethod
    {
        get { return _soapRequestCompressionMethod; }
        set
        {
            if (_soapRequestCompressionMethod != value)
            {
                _soapRequestCompressionMethod = value;
                NotifyOfPropertyChange(nameof(SoapRequestCompressionMethod));
            }
        }
    }

    private bool _soapAcceptCompressedResponses;

    [DataMember]
    public bool SoapAcceptCompressedResponses
    {
        get { return _soapAcceptCompressedResponses; }
        set
        {
            if (_soapAcceptCompressedResponses != value)
            {
                _soapAcceptCompressedResponses = value;
                NotifyOfPropertyChange(nameof(SoapAcceptCompressedResponses));
            }
        }
    }

    private string _proxyHost;

    [DataMember]
    public string ProxyHost
    {
        get { return _proxyHost; }
        set
        {
            if (!string.Equals(_proxyHost, value))
            {
                _proxyHost = value;
                NotifyOfPropertyChange(nameof(ProxyHost));
            }
        }
    }

    private int _proxyPort;

    [DataMember]
    public int ProxyPort
    {
        get { return _proxyPort; }
        set
        {
            if (_proxyPort != value)
            {
                _proxyPort = value;
                NotifyOfPropertyChange(nameof(ProxyPort));
            }
        }
    }

    private int _redirectPort;

    [DataMember]
    public int RedirectPort
    {
        get { return _redirectPort; }
        set
        {
            if (_redirectPort != value)
            {
                _redirectPort = value;
                NotifyOfPropertyChange(nameof(RedirectPort));
            }
        }
    }

    public override string ToString()
    {
        return $"Uri: {Uri}; Username: {Username}; AuthenticationType: {AuthenticationType}; SecurityProtocol: {SecurityProtocol};" +
               $" ProxyHost: {ProxyHost}; ProxyPort: {ProxyPort};" +
               $" SoapRequestCompressionMethod: {SoapRequestCompressionMethod};" +
               $" SoapAcceptCompressedResponses: {SoapAcceptCompressedResponses};";
    }

    #region INotifyPropertyChanged Members
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyOfPropertyChange(string info)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }
    #endregion INotifyPropertyChanged Members
}
