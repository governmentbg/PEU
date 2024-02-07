<xsl:stylesheet version="1.0" xmlns:CWFGG="http://ereg.egov.bg/segment/R-3145"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
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
                xmlns:AM="http://ereg.egov.bg/segment/R-3119"
				        xmlns:PD="http://ereg.egov.bg/segment/R-3037"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
                xmlns:AFIRCWFGGPD="http://ereg.egov.bg/segment/R-3111"
                xmlns:ЕМА="http://ereg.egov.bg/segment/R-3110"
                xmlns:CPD="http://ereg.egov.bg/segment/R-3112"
                xmlns:xslExtension="urn:XSLExtension"
                
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./PBZNBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGasses">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>

        <xsl:choose>
          <xsl:when test="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
            <table align="center" cellpadding="3" width= "700px" >
              <tr>
                <th colspan="2">
                  <h2 class="uppercase">
                    <xsl:value-of select="CWFGG:ElectronicServiceProviderBasicData/ESPBD:EntityBasicData/E:Name" />
                  </h2>
                  <h4>ГЛАВНА ДИРЕКЦИЯ  „ПОЖАРНА БЕЗОПАСНОСТ И ЗАЩИТА НА НАСЕЛЕНИЕТО”</h4>
                  <h5>
                    <hr/>1309 София, ул. „Пиротска” № 171А, тел.: +359 2/ 982 12 22, e-mail:nspab@mvr.bg
                  </h5>
                  <h3>
                    &#160;<xsl:value-of select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesHeader" />
                  </h3>
                </th>
              </tr>
              <tr>
                <td colspan="2">
                  <p align="center">
                    <b>
                      №&#160;<xsl:value-of select="CWFGG:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="CWFGG:DocumentURI/DURI:SequenceNumber" />-<xsl:choose>
                        <xsl:when test="CWFGG:DocumentURI/DURI:ReceiptOrSigningDate">
                          <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentURI/DURI:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                        </xsl:when>
                      </xsl:choose>
                    </b>
                  </p>
                </td>
              </tr>
              <tr>
                <td align="center" colspan="2">
                  <b>
                    На&#160;
                    <xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>
                  </b>
                </td>
              </tr>
              <tr>
                <td align="center" colspan="2">
                  <b>
                    адрес:
                    &#160;
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:DistrictName">
                        Обл.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:DistrictName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:MunicipalityName">
                        Общ.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:MunicipalityName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:SettlementName">
                        гр./с.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:SettlementName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:AreaName">
                        р-н.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:AreaName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:PostCode">
                        п.к.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:PostCode"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:HousingEstate">
                        ж.к.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:HousingEstate"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Street">
                        бул./ул.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Street"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:StreetNumber">
                        №&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:StreetNumber"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Block">
                        бл.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Block"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Entrance">
                        вх.<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Entrance"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Floor">
                        ет.<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Floor"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Apartment">
                        ап.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:EntityManagementAddress/CADR:Apartment"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                  </b>
                </td>
              </tr>
              <tr>
                <td align="center" colspan="2">
                  <b>
                    ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>&#160;
                  </b>
                </td >
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesActivities" disable-output-escaping="yes"/>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <b>
                    Валидност:&#160;<xsl:value-of  select="CWFGG:CertificateValidity"/>
                  </b>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesGround"  disable-output-escaping="yes"/>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  ПРИЛОЖЕНИЕ: Списък на сертифициран персонал
                </td>
              </tr>
              <tr>
                <td align="left" width="50%">
                  Дата:
                  <xsl:choose>
                    <xsl:when test="CWFGG:DocumentReceiptOrSigningDate">
                      <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentReceiptOrSigningDate, 'dd.MM.yyyy')"/>г.
                    </xsl:when>
                  </xsl:choose>
                </td>
                <td width="50%">
                  <table>
                    <tr>
                      <td>ДИРЕКТОР:</td>
                      <td>
                        <xsl:call-template name="DocumentSignatures">
                          <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                        </xsl:call-template>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            </table>
            <table class="page-br" align="center" cellpadding="3" width= "700px" >
              <tr>
                <th colspan="2">
                  <h2 class="uppercase">
                    <xsl:value-of select="CWFGG:ElectronicServiceProviderBasicData/ESPBD:EntityBasicData/E:Name" />
                  </h2>
                  <h4>ГЛАВНА ДИРЕКЦИЯ  „ПОЖАРНА БЕЗОПАСНОСТ И ЗАЩИТА НА НАСЕЛЕНИЕТО”</h4>
                  <h5>
                    <hr/>1309 София, ул. „Пиротска” № 171А, тел.: +359 2/ 982 12 22, e-mail:nspab@mvr.bg
                  </h5>
                </th>
              </tr>             
              <tr>
                <td  align="center" colspan="2">
                  <h3>СПИСЪК</h3>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <b>
                    със сертифициран персонал на
                  </b>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <b>
                    <xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>
                  </b>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <b>
                    № 1 от&#160;<xsl:choose>
                      <xsl:when test="CWFGG:DocumentReceiptOrSigningDate">
                        <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentReceiptOrSigningDate, 'dd.MM.yyyy')"/>г.
                      </xsl:when>
                    </xsl:choose>
                  </b>
                </td>
              </tr>
              <xsl:choose>
                <xsl:when test = "CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:AvailableCertifiedPersonnel">
                  <tr>
                    <td colspan="2">
                      <ol>
                        <xsl:for-each select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesEntityData/ЕМА:AvailableCertifiedPersonnel/ЕМА:CertifiedPersonel">
                          <li>
                            <xsl:value-of select="CPD:PersonFirstName" />&#160;<xsl:value-of select="CPD:PersonLastName" /> <br/>
                            Документ за правоспособност (сертификат) № &#160;<xsl:value-of select="CPD:CertificateNumber" />
                          </li>
                        </xsl:for-each>
                      </ol>
                    </td>
                  </tr>
                </xsl:when>
              </xsl:choose>
              <tr>
                <td colspan="2">
                  ПРИЛОЖЕНИЕ към Документ за правоспособност (сертификат)
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  №&#160;<xsl:value-of select="CWFGG:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="CWFGG:DocumentURI/DURI:SequenceNumber" />-<xsl:choose>
                    <xsl:when test="CWFGG:DocumentURI/DURI:ReceiptOrSigningDate">
                      <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentURI/DURI:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                    </xsl:when>
                  </xsl:choose>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonsGround"  disable-output-escaping="yes"/>
                </td>
              </tr>
              <tr>
                <td align="left" width="50%">
                  Дата:
                  <xsl:choose>
                    <xsl:when test="CWFGG:DocumentReceiptOrSigningDate">
                      <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentReceiptOrSigningDate, 'dd.MM.yyyy')"/>г.
                    </xsl:when>
                  </xsl:choose>
                </td>
                <td width="50%">
                  <table>
                    <tr>
                      <td>ДИРЕКТОР:</td>
                      <td>
                        <xsl:call-template name="DocumentSignatures">
                          <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                        </xsl:call-template>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </xsl:when>
          <xsl:otherwise>
            <table align="center" cellpadding="3" width= "700px" >
              <tr>
                <th colspan="2">
                  <h2 class="uppercase">
                    <xsl:value-of select="CWFGG:ElectronicServiceProviderBasicData/ESPBD:EntityBasicData/E:Name" />
                  </h2>
                  <h4>
                    ГЛАВНА ДИРЕКЦИЯ  „ПОЖАРНА БЕЗОПАСНОСТ И ЗАЩИТА НА НАСЕЛЕНИЕТО”
                  </h4>
                  <h5>
                    <hr/>1309 София, ул. „Пиротска” № 171А, тел.: +359 2/ 982 12 22, e-mail:nspab@mvr.bg
                  </h5>
                  <h3>
                    &#160;<xsl:value-of select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesHeader" />
                  </h3>
                </th>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesActivities" disable-output-escaping="yes"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <p align="center">
                    <b>
                      №&#160;<xsl:value-of select="CWFGG:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="CWFGG:DocumentURI/DURI:SequenceNumber" />-<xsl:choose>
                        <xsl:when test="CWFGG:DocumentURI/DURI:ReceiptOrSigningDate">
                          <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentURI/DURI:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                        </xsl:when>
                      </xsl:choose>
                    </b>
                  </p>
                </td>
              </tr>
              <tr>
                <td align="center">
                  <b>
                    На&#160;
                    <xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First"/>&#160;
                    <xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle"/>&#160;
                    <xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last"/>&#160;
                  </b>
                </td>
                <td  align="center" rowspan ="3">
                  <xsl:choose>
                    <xsl:when test="CWFGG:IdentificationPhoto/.">
                      <img  width="150" height="200">
                        <xsl:attribute name="src" >
                          <xsl:value-of select="concat('data:image/gif;base64,',CWFGG:IdentificationPhoto)"/>
                        </xsl:attribute>
                      </img>
                    </xsl:when>
                    <xsl:otherwise>
                      <img src="nophoto" width="150" height="200"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </td>
              </tr>
              <tr>
                <td align="center">
                  <b>
                    ЕГН/ЛНЧ:&#160;<xsl:value-of  select="CWFGG:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>&#160;
                  </b>
                </td >
              </tr>
              <tr>
                <td align="center">
                  <b>
                    адрес:&#160;
                    <xsl:choose>
                      <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress">
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName ">
                            Обл.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:MunicipalityGRAOName ">
                            Общ.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:MunicipalityGRAOName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName ">
                            гр./с.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:DistrictGRAOName"/>&#160;<br/>
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:StreetText ">
                            ул.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:StreetText"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:BuildingNumber ">
                            №&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:BuildingNumber"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Entrance ">
                            вх.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Entrance"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Floor ">
                            ет.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Floor"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Apartment ">
                            ап.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:PermanentAddress/PA:Apartment"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName ">
                            Обл.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:MunicipalityGRAOName ">
                            Общ.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:MunicipalityGRAOName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName ">
                            гр./с.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:DistrictGRAOName"/>&#160;<br/>
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:StreetText ">
                            ул.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:StreetText"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:BuildingNumber ">
                            №&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:BuildingNumber"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Entrance ">
                            вх.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Entrance"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Floor ">
                            ет.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Floor"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Apartment ">
                            ап.&#160;<xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesPersonData/AFIRCWFGGPD:CurrentAddress/PA:Apartment"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                      </xsl:otherwise>
                    </xsl:choose>
                  </b>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <b>
                    Валидност:&#160;<xsl:value-of  select="CWFGG:CertificateValidity"/>
                  </b>
                </td>
              </tr>
              <tr>
                <td  align="center" colspan="2">
                  <xsl:value-of  select="CWFGG:CertificateToWorkWithFluorinatedGreenhouseGassesGround"  disable-output-escaping="yes"/>
                </td>
              </tr>
              <tr>
                <td align="left" width="50%">
                  Дата:
                  <xsl:choose>
                    <xsl:when test="CWFGG:DocumentReceiptOrSigningDate">
                      <xsl:value-of select="xslExtension:FormatDate(CWFGG:DocumentReceiptOrSigningDate, 'dd.MM.yyyy')"/>г.
                    </xsl:when>
                  </xsl:choose>
                </td>
                <td width="50%">
                  <table>
                    <tr>
                      <td>ДИРЕКТОР:</td>
                      <td>
                        <xsl:call-template name="DocumentSignatures">
                          <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                        </xsl:call-template>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            </table>
          </xsl:otherwise>
        </xsl:choose>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>