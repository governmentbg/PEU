<xsl:stylesheet version="1.0" xmlns:AFIRCWFGG="http://ereg.egov.bg/segment/R-3107"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AFIRCWFGGD="http://ereg.egov.bg/segment/R-3106"
				        xmlns:AFIRCWFGGED="http://ereg.egov.bg/segment/R-3110"
				        xmlns:AFIRCWFGGPD="http://ereg.egov.bg/segment/R-3111"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:PD="http://ereg.egov.bg/segment/R-3037"
				        xmlns:E="http://ereg.egov.bg/segment/0009-000013"
				        xmlns:CADR="http://ereg.egov.bg/segment/R-3203"
				        xmlns:CP="http://ereg.egov.bg/segment/R-3112"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:AA="http://ereg.egov.bg/segment/0009-000141"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./PBZNBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>

        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th>
                &#160;
              </th>
              <th>
                <p align="right">
                  ДО<br/>
                  ДИРЕКТОРА НА<br/>
                  <xsl:value-of select="AFIRCWFGG:IssuingPoliceDepartment/PD:PoliceDepartmentName" />
                </p>
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                <h2>
                  <xsl:value-of select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
          </thead>
          <tbody >
            <tr>
              <td colspan ="2">
                <table width ="100%" style="border: 1px solid black;">
                  <xsl:choose>
                    <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                      <tr>
                        <td>
                          От:&#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
                          &#160;
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/.">
                              <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          &#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>
                        </td >
                      </tr>
                      <tr>
                        <td>
                          ЕГН/ЛНЧ:&#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,&#160;
                        </td>

                      </tr>
                      <tr>
                        <td>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                              Лична карта
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                              Паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                              Дипломатически паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                              Служебен паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                              Моряшки паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                              Военна карта за самоличност
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                              Свидетелство за управление на моторно превозно средство
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                              Временен паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                              Служебен открит лист за преминаване на границата
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                              Карта на бежанец
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                              Карта на чужденец с хуманитарен статут
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                              Карта на чужденец, получил убежище
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                              Разрешение за пребиваване
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                              Удостоверение за пребиваване на гражданин на ЕС
                            </xsl:when>
                          </xsl:choose>
                          &#160;№:&#160;<xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityNumber"/>
                          <br/>
                          изд. на:&#160;
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentitityIssueDate">
                              <xsl:value-of select="xslExtension:FormatDate(AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentitityIssueDate, 'dd.MM.yyyy')"/>г.
                            </xsl:when>
                          </xsl:choose>
                          &#160;от &#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          на:&#160;<xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Седалище и адрес на управление:&#160;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:DistrictName">
                              Обл.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:DistrictName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:MunicipalityName">
                              Общ.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:MunicipalityName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:SettlementName">
                              гр./с.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:SettlementName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:AreaName">
                              р-н.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:AreaName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:PostCode">
                              п.к.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:PostCode"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:HousingEstate">
                              ж.к.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:HousingEstate"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Street">
                              бул./ул.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Street"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:StreetNumber">
                              №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:StreetNumber"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Block">
                              бл.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Block"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Entrance">
                              вх.<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Entrance"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Floor">
                              ет.<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Floor"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Apartment">
                              ап.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:EntityManagementAddress/CADR:Apartment"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td colspan ="2">
                          Адрес за кореспонденция:&#160;
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:DistrictName">
                              Обл.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:DistrictName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:MunicipalityName">
                              Общ.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:MunicipalityName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:SettlementName">
                              гр./с.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:SettlementName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:AreaName">
                              р-н.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:AreaName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:PostCode">
                              п.к.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:PostCode"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:HousingEstate">
                              ж.к.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:HousingEstate"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Street">
                              бул./ул.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Street"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:StreetNumber">
                              №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:StreetNumber"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Block">
                              бл.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Block"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Entrance">
                              вх.<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Entrance"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Floor">
                              ет.<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Floor"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Apartment">
                              ап.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:CorrespondingAddress/CADR:Apartment"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:WorkPhone">
                              Телефон:&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:WorkPhone"/>,&#160;
                            </xsl:when>
                          </xsl:choose>
                          е-mail:&#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
                        </td>
                      </tr>
                    </xsl:when>
                    <xsl:otherwise>
                      <tr>
                        <td>
                          Oт&#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                          &#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
                          &#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          ЕГН/ЛНЧ:&#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>&#160;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                              Лична карта
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                              Паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                              Дипломатически паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                              Служебен паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                              Моряшки паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                              Военна карта за самоличност
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                              Свидетелство за управление на моторно превозно средство
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                              Временен паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                              Служебен открит лист за преминаване на границата
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                              Карта на бежанец
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                              Карта на чужденец с хуманитарен статут
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                              Карта на чужденец, получил убежище
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                              Разрешение за пребиваване
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                              Удостоверение за пребиваване на гражданин на ЕС
                            </xsl:when>
                          </xsl:choose>
                          &#160;№:&#160;<xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/>
                          <br/>
                          изд. на:&#160;
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate">
                              <xsl:value-of select="xslExtension:FormatDate(AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate, 'dd.MM.yyyy')"/>г.
                            </xsl:when>
                          </xsl:choose>
                          &#160;от &#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Постоянен адрес:&#160;
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName ">
                              Обл.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:MunicipalityGRAOName ">
                              Общ.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:MunicipalityGRAOName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName ">
                              гр./с.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName"/>&#160;<br/>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:StreetText ">
                              ул.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:StreetText"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:BuildingNumber ">
                              №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:BuildingNumber"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Entrance ">
                              вх.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Entrance"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Floor ">
                              ет.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Floor"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Apartment ">
                              ап.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Apartment"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Настоящ адрес:&#160;
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName ">
                              Обл.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:MunicipalityGRAOName ">
                              Общ.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:MunicipalityGRAOName"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName ">
                              гр./с.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName"/>&#160;<br/>
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:StreetText ">
                              ул.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:StreetText"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:BuildingNumber ">
                              №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:BuildingNumber"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Entrance ">
                              вх.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Entrance"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Floor ">
                              ет.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Floor"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Apartment ">
                              ап.&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Apartment"/>&#160;
                            </xsl:when>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:WorkPhone">
                              Телефон:&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:WorkPhone"/>,&#160;
                            </xsl:when>
                          </xsl:choose>
                          е-mail:&#160;
                          <xsl:value-of  select="AFIRCWFGG:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
                        </td>
                      </tr>
                    </xsl:otherwise>
                  </xsl:choose>
                </table>
              </td>
            </tr>

            <xsl:choose>
              <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData">
                <tr>
                  <td colspan ="2">
                    <table style="border: 1px solid black; width:100%">
                      <tr>
                        <td align="center">
                          <u>
                            <b>Правоспособност на заявителя:</b>
                          </u>
                        </td>
                      </tr>
                      <xsl:choose>
                        <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CertificateType = '2001'">
                          <tr>
                            <td align="center">
                              <u>
                                <b>При ИЗДАВАНЕ на документ за правоспособност</b>
                              </u>
                            </td>
                          </tr>
                          <tr>
                            <td align="center">
                              Удостоверение №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CertificateNumber"/>&#160; за завършен курс.
                            </td>
                          </tr>
                          <xsl:choose>
                            <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:DiplomaNumber">
                              <tr>
                                <td align="center">
                                  Диплома №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:DiplomaNumber"/>&#160;за завършено образование.
                                </td>
                              </tr>
                            </xsl:when>
                          </xsl:choose>
                        </xsl:when>
                        <xsl:when test="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CertificateType = '2002'">
                          <tr>
                            <td align="center">
                              <u>
                                <b>При ПОДНОВЯВАНЕ на документ за правоспособност</b>
                              </u>
                            </td>
                          </tr>
                          <tr>
                            <td align="center">
                              Удостоверение №&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CertificateNumber"/>&#160;за положен изпит.
                            </td>
                          </tr>
                        </xsl:when>
                      </xsl:choose>
                    </table>
                  </td>
                </tr>
              </xsl:when>
              <xsl:otherwise>
                <tr>
                  <td colspan="2">
                    <table style="border: 1px solid black; width:100%">
                      <tr>
                        <td>
                          ЗАЯВЕН ОБХВАТ ЗА СЕРТИФИКАЦИЯ:&#160;<xsl:value-of  select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:DeclaredScopeOfCertification"/>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <table style="border: 1px solid black; width:100%">
                      <tr>
                        <td>НАЛИЧЕН СЕРТИФИЦИРАН ПЕРСОНАЛ:</td>
                      </tr>
                      <tr>
                        <td>
                          <ol>
                            <xsl:for-each select="AFIRCWFGG:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData/AFIRCWFGGD:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData/AFIRCWFGGED:AvailableCertifiedPersonnel/AFIRCWFGGED:CertifiedPersonel">
                              <li>
                                <xsl:value-of  select="CP:PersonFirstName"/>&#160;
                                <xsl:value-of  select="CP:PersonLastName"/>, Номер на сертификат:
                                <xsl:value-of  select="CP:CertificateNumber"/>
                              </li>
                            </xsl:for-each>
                          </ol>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </xsl:otherwise>
            </xsl:choose>
            <tr>
              <td colspan ="2">
                Желая да получа искания документ:
                <xsl:choose>
                  <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000076' ">
                    &#160;като електронен документ
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000077' ">
                    &#160;на мястото на заявяване
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000079' ">
                    &#160;на посочения в искането адрес, чрез лицензиран пощенски оператор, като декларирам, че пощенските разходи са за моя сметка
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000080' ">
                    &#160;чрез лицензиран пощенски оператор на адрес:&#160;
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:DistrictName">
                        Обл.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:DistrictName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:MunicipalityName">
                        Общ.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:MunicipalityName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:SettlementName">
                        гр./с.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:SettlementName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:AreaName">
                        р-н.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:AreaName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:PostCode">
                        п.к.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:PostCode"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:HousingEstate">
                        ж.к.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:HousingEstate"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Street">
                        бул./ул.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Street"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:StreetNumber">
                        №&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:StreetNumber"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Block">
                        бл.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Block"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Entrance">
                        вх.<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Entrance"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Floor">
                        ет.<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Floor"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Apartment">
                        ап.&#160;<xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Apartment"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AFIRCWFGG:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:AddressDescription"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "AFIRCWFGG:Declarations">
                <xsl:for-each select="AFIRCWFGG:Declarations/AFIRCWFGG:Declaration">
                  <xsl:choose>
                    <xsl:when test="DECL:IsDeclarationFilled = 'true'">
                      <tr>
                        <td colspan="2">
                          <xsl:value-of  select="DECL:DeclarationName" disable-output-escaping="yes"/>
                        </td>
                      </tr>
                      <xsl:choose>
                        <xsl:when test="DECL:FurtherDescriptionFromDeclarer">
                          <tr>
                            <td colspan="2">
                              Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
                            </td>
                          </tr>
                          <tr>
                            <td colspan="2">
                              <xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
                            </td>
                          </tr>
                        </xsl:when>
                      </xsl:choose>
                    </xsl:when>
                  </xsl:choose>

                </xsl:for-each>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test = "AFIRCWFGG:AttachedDocuments">
                <tr>
                  <td colspan="2">
                    Приложени документи:
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="AFIRCWFGG:AttachedDocuments/AFIRCWFGG:AttachedDocument">
                        <li>
                          <xsl:value-of select="ADD:AttachedDocumentDescription" />
                        </li>
                      </xsl:for-each>
                    </ol>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td width="50%">
                Дата:
                <xsl:choose>
                  <xsl:when test="AFIRCWFGG:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime">
                    <xsl:value-of select="xslExtension:FormatDate(AFIRCWFGG:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime, 'dd.MM.yyyy')"/>г.
                  </xsl:when>
                </xsl:choose>
              </td>
              <td width="50%">
                <xsl:call-template name="DocumentSignatures">
                  <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                </xsl:call-template>
              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>