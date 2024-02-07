<xsl:stylesheet version="1.0" xmlns:RFILFPSS="http://ereg.egov.bg/segment/R-3108"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:RFILFPSSD="http://ereg.egov.bg/segment/R-3109"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:CADR="http://ereg.egov.bg/segment/R-3203"
				        xmlns:DISTR="http://ereg.egov.bg/value/R-2120"
				        xmlns:TSS="http://ereg.egov.bg/value/R-2120"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./CODBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServices">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                <h2>
                  <xsl:value-of select="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
              </th>
            </tr>
            <tr>
              <th>
                &#160;
              </th>
              <th >
                <p align="right">
                  ДО<br/>
                  ДИРЕКТОРА НА<br/>
                  ГЛАВНА ДИРЕКЦИЯ<br/>
                  "НАЦИОНАЛНА ПОЛИЦИЯ"<br/>
                  ГР.СОФИЯ-1715<br/>
                  БУЛ. "АЛЕКСАНДЪР МАЛИНОВ",№ 1
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
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                ЗАЯВЛЕНИЕ
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                От:&#160;
                <xsl:value-of  select="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
                &#160;
                <xsl:choose>
                  <xsl:when test="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/.">
                    <xsl:value-of  select="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
                  </xsl:when>
                </xsl:choose>
                &#160;
                <xsl:value-of  select="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>, управляващ
              </td >
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:value-of  select="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                ЕИК / БУЛСТАТ:&#160;<xsl:value-of  select="RFILFPSS:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>,
                &#160;със седалище и адрес на управление:
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:DistrictName">
                    Обл.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:DistrictName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:MunicipalityName">
                    Общ.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:MunicipalityName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:SettlementName">
                    гр./с.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:SettlementName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:AreaName">
                    р-н.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:AreaName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:PostCode">
                    п.к.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:PostCode"/>&#160;
                  </xsl:when>
                </xsl:choose>
                  <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:HousingEstate">
                    ж.к.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:HousingEstate"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Street">
                    бул./ул.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Street"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:StreetNumber">
                    №&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:StreetNumber"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Block">
                   бл.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Block"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Entrance">
                    вх.<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Entrance"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Floor">
                    ет.<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Floor"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Apartment">
                    ап.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:EntityManagementAddress/CADR:Apartment"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                адрес за кореспонденция:&#160;
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:DistrictName">
                    Обл.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:DistrictName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:MunicipalityName">
                    Общ.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:MunicipalityName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:SettlementName">
                    гр./с.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:SettlementName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:AreaName">
                    р-н.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:AreaName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:PostCode">
                    п.к.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:PostCode"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:HousingEstate">
                    ж.к.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:HousingEstate"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Street">
                    бул./ул.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Street"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:StreetNumber">
                    №&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:StreetNumber"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Block">
                    бл.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Block"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Entrance">
                    вх.<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Entrance"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Floor">
                    ет.<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Floor"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Apartment">
                    ап.&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:CorrespondingAddress/CADR:Apartment"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:MobilePhone">
                    Телефон:&#160;<xsl:value-of  select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:MobilePhone"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>

            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td align="center" colspan ="2">
                ГОСПОДИН ДИРЕКТОР,
              </td>

            </tr>
            <tr>
              <td colspan ="2">
                &#160;&#160;&#160;&#160;&#160;Моля да ни бъде издаден лиценз за извършване на частна охранителна дейност по:
                <ul>
                  <xsl:for-each select="RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:SecurityServiceTypes">
                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-301' ">
                        <li>
                          чл.5, ал.1 т.1 : Лична охрана на физически лица<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>

                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-302' ">
                        <li>
                          чл.5, ал.1 т.2 : Охрана на имуществото на физически или юридически лица<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>

                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-307' ">
                        <li>
                          чл.5, ал.1 т.3 : Сигнално-охранителна дейност<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-305' ">
                        <li>
                          чл.5, ал.1 т.4 : Самоохрана на имущество на лица по чл. 2, ал. 3<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-308' ">
                        <li>
                          чл.5, ал.1 т.5 : Охрана на обекти - недвижими имоти<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>

                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-303' ">
                        <li>
                          чл.5, ал.1 т.7 : Охрана на мероприятия<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>

                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-304' ">
                        <li>
                          чл.5, ал.1 т.8 : Охрана при транспортиране на ценни пратки или товари<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="RFILFPSSD:PointOfPrivateSecurityServicesLaw = 'R-309' ">
                        <li>
                          чл.5, ал.1 т.9 :  Охрана на селскостопанско имущество<br/>
                          от Закона за частната охранителна дейност, за територията на:<br/>
                          <xsl:choose>
                            <xsl:when test="RFILFPSSD:TerritorialScopeOfServices/TSS:ScopeOfCertification = '2'">
                              <ul>
                                <xsl:for-each select="RFILFPSSD:TerritorialScopeOfServices/TSS:Districts">
                                  <li>
                                    Област:&#160;<xsl:value-of  select="DISTR:DistrictGRAOName"/>
                                  </li>
                                </xsl:for-each>
                              </ul>
                            </xsl:when>
                            <xsl:otherwise>
                              <ul>
                                <li>цялата страна</li>
                              </ul>
                            </xsl:otherwise>
                          </xsl:choose>
                        </li>
                      </xsl:when>
                    </xsl:choose>

                  </xsl:for-each>
                </ul>
              </td>

            </tr>

            <xsl:choose>
              <xsl:when test = "RFILFPSS:RequestForIssuingLicenseForPrivateSecurityServicesData/RFILFPSSD:AgreementToReceiveERefusal = 'true'">
                <tr>
                  <td colspan = "2">
                    В случай на отказ от предоставяне на услугата, желая да получа отказа като електронен документ, съгласно чл.25, ал.2 от Закона за електронното управление.
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>

            <xsl:choose>
              <xsl:when test = "RFILFPSS:Declarations">
                <xsl:for-each select="RFILFPSS:Declarations/RFILFPSS:Declaration">
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
              <xsl:when test = "RFILFPSS:AttachedDocuments">
                <tr>
                  <td colspan="2">
                    Приложени документи:
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="RFILFPSS:AttachedDocuments/RFILFPSS:AttachedDocument">
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
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td width="50%">
                Дата:&#160;<xsl:value-of  select="ms:format-date(RFILFPSS:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
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