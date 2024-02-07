<xsl:stylesheet version="1.0" xmlns:AFIDVO="http://ereg.egov.bg/segment/R-3104"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AFIDVOD="http://ereg.egov.bg/segment/R-3105"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnership">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                <h2>
                  <xsl:value-of select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
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
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                    От:&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    &#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>,
                    &#160;
                    ЕГН/ЛНЧ
                    &#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>
                  </xsl:when>
                  <xsl:otherwise>
                    От&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                    &#160;
                    ЕГН/ЛНЧ
                    &#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                  </xsl:otherwise>
                </xsl:choose>
              </td >
            </tr>



            <xsl:choose>
              <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                <tr>
                  <td colspan="2">
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>
                    &#160;№&#160;<xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityNumber"/><br/>
                    изд. на&#160;
                    <xsl:value-of  select="ms:format-date(AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.
                    &#160;от&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                </tr>
              </xsl:when>
              <xsl:otherwise>
                <tr>
                  <td colspan="2">
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>

                    &#160;№&#160;<xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/><br/>
                    изд. на&#160;
                    <xsl:value-of  select="ms:format-date(AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.
                    &#160;от&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                </tr>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress">
                <tr>
                  <td colspan ="2">
                    Постоянен адрес:&#160;
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:DistrictGRAOName">
                        Обл.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:DistrictGRAOName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:MunicipalityGRAOName">
                        Общ.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:MunicipalityGRAOName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:SettlementGRAOName">
                        &#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:SettlementGRAOName"/>&#160;<br/>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:StreetText">
                        ул.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:StreetText"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:BuildingNumber">
                        №&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:BuildingNumber"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:Entrance">
                        вх.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:Entrance"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:Floor">
                        ет.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:Floor"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:Apartment">
                        ап.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:PermanentAddress/PA:Apartment"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td colspan ="2">
                Настоящ адрес:&#160;
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:DistrictGRAOName ">
                    Обл.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:DistrictGRAOName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:MunicipalityGRAOName ">
                    Общ.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:MunicipalityGRAOName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:SettlementGRAOName ">
                    &#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:SettlementGRAOName"/>&#160;<br/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:StreetText ">
                    ул.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:StreetText"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:BuildingNumber ">
                    №&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:BuildingNumber"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:Entrance ">
                    вх.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:Entrance"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:Floor ">
                    ет.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:Floor"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:Apartment ">
                    ап.&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:CurrentAddress/PA:Apartment"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress">
                    Адрес на електронна поща:&#160;<xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
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
                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                <xsl:choose>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1001'">
                    Моля
                  </xsl:when>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002'">
                    Моля, в качеството на пълномощник,
                  </xsl:when>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1003'">
                    Моля, в качеството на законен представител,
                  </xsl:when>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1004'">
                    Моля, в качеството на длъжностно лице,
                  </xsl:when>
                </xsl:choose>
                да ми бъде издадено удостоверение за
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:DocumentFor = '2001' ">
                    собственост на пътно превозно средство със следните данни
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:DocumentFor = '2002' ">
                    бивша собственост на пътно превозно средство със следните данни
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:DocumentFor = '2003' ">
                    собственост на всички превозни средства
                  </xsl:when>
                </xsl:choose>
              </td>

            </tr>
            <xsl:choose>
              <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:DocumentFor != '2003' ">
                <tr>
                  <td colspan ="2">
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:RegistrationAndMake/AFIDVOD:RegistrationNumber">
                        Регистрационен номер&#160; <xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:RegistrationAndMake/AFIDVOD:RegistrationNumber"/>
                      </xsl:when>
                    </xsl:choose>
                  </td>
                </tr>
                <tr>
                  <td colspan ="2">
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:RegistrationAndMake/AFIDVOD:MakeModel">
                        Марка / модел:&#160;<xsl:value-of  select="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:RegistrationAndMake/AFIDVOD:MakeModel"/>
                      </xsl:when>
                    </xsl:choose>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                    Собственост на&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                    Собственост на&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">

                <xsl:choose>
                  <xsl:when test="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                    ЕИК/БУЛСТАТ:&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier/."/>,
                  </xsl:when>
                  <xsl:otherwise>
                    ЕГН/ЛНЧ:&#160;
                    <xsl:value-of  select="AFIDVO:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>,
                  </xsl:otherwise>
                </xsl:choose>

              </td>
            </tr>




            <tr>
              <td colspan = "2">
                Същото да послужи:&#160;
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2001' ">
                    други
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2002' ">
                    пред	застрахователни дружества
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2003' ">
                    пред	консулски служби
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2004' ">
                    за	министерство на транспорта
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2005' ">
                    пред	митнически органи
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2006' ">
                    пред  нотариус
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2007' ">
                    пред	съдебни власти
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDVO:ApplicationForIssuingDocumentofVehicleOwnershipData/AFIDVOD:OwnershipCertificateReason = '2008' ">
                    пред финансови органи
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "AFIDVO:Declarations">
                <xsl:for-each select="AFIDVO:Declarations/AFIDVO:Declaration">
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
              <xsl:when test = "AFIDVO:AttachedDocuments">
                <tr>
                  <td colspan="2">
                    Приложени документи:
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="AFIDVO:AttachedDocuments/AFIDVO:AttachedDocument">
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
                Дата:&#160;<xsl:value-of  select="ms:format-date(AFIDVO:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
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