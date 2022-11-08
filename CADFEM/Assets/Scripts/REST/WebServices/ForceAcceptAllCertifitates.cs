using UnityEngine.Networking;
// Overrides standard certificate verification method. when requesting a certificate, method allways returns certificate validation  => true 
public class ForceAcceptAllCertificates : CertificateHandler {
    protected override bool ValidateCertificate(byte[] certificateData) => true;
}