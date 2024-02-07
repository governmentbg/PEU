<xsl:stylesheet version="1.0" xmlns:CAEFRIID="http://ereg.egov.bg/segment/R-3046"
                xmlns:ESPD="http://ereg.egov.bg/segment/0009-000002"
								xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
								xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
								xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
								xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
								xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
								xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
								xmlns:IPD="http://ereg.egov.bg/segment/R-3037"
                xmlns:IDD="http://ereg.egov.bg/segment/R-3007"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:RD="http://ereg.egov.bg/value/R-2054"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ACU="http://ereg.egov.bg/segment/0009-000044"

xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./BDSBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="CAEFRIID:CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <body>
        <xsl:call-template name="Head"/>
        <table align="center" cellpadding="5" width= "700px" >
          <tr>
            <th colspan ="2">
              <h2 class="uppercase">
                <p>
                  <xsl:value-of select="CAEFRIID:ElectronicServiceProviderBasicData/ESPD:EntityBasicData/EBD:Name" />
                </p>
                <p>
                  <xsl:value-of select="CAEFRIID:IssuingPoliceDepartment/IPD:PoliceDepartmentName" />
                </p>
                <hr/>
              </h2>
            </th>
          </tr>
          <tr>
            <td colspan ="2" align="center">
              <h3>
                <b>
                  <xsl:value-of select="CAEFRIID:CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader" />&#160;№:&#160;
                  <xsl:value-of select="CAEFRIID:DocumentURI/DU:RegisterIndex" />
                  -<xsl:value-of select="CAEFRIID:DocumentURI/DU:SequenceNumber" />
                  -<xsl:value-of  select="ms:format-date(CAEFRIID:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                </b>
                <br/>
                по преписка с №:&#160;
                <xsl:value-of select="CAEFRIID:AISCaseURI/ACU:DocumentURI/DU:RegisterIndex" />
                -<xsl:value-of select="CAEFRIID:AISCaseURI/ACU:DocumentURI/DU:SequenceNumber" />
                -<xsl:value-of  select="ms:format-date(CAEFRIID:AISCaseURI/ACU:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
              </h3>
            </td>
          </tr>

          <tr>
            <td colspan ="2">
              <p style="text-align:justify;" >
                <xsl:value-of select="CAEFRIID:IssuingPoliceDepartment/IPD:PoliceDepartmentName" />
                издава настоящото на
                <xsl:value-of  select="CAEFRIID:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                &#160;<xsl:choose>
                  <xsl:when test="CAEFRIID:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle">
                    <xsl:value-of  select="CAEFRIID:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
                    &#160;
                  </xsl:when>
                </xsl:choose><xsl:value-of  select="CAEFRIID:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>&#160;,
                &#160;
                ЕГН&#160;<xsl:value-of  select="CAEFRIID:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/ID:EGN/."/>,&#160;
                <xsl:choose>
                  <xsl:when test="CAEFRIID:CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData">
                    <xsl:value-of select="CAEFRIID:CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData" disable-output-escaping="yes" />
                  </xsl:when>
                </xsl:choose>
              </p>
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              &#160;
            </td>
          </tr>
          <tr>
            <td colspan ="2" align="left">
              <xsl:choose>
                <xsl:when test="CAEFRIID:IdentityDocuments and CAEFRIID:IdentityDocuments!=''">
                  <table border = "1px" style="width:100%">
                    <tr>
                      <th style="padding: 5px">
                        Вид <br/> документ
                      </th>
                      <th style="padding: 5px">
                        Номер
                      </th>
                      <th style="padding: 5px">
                        Поделение <br/> издало
                      </th>
                      <th style="padding: 5px">
                        Дата <br/> издаване
                      </th>
                      <th style="padding: 5px">
                        Дата <br/> валидност
                      </th>
                      <th style="padding: 5px">
                        Актуален <br/> статус
                      </th>
                    </tr>

                    <xsl:for-each select="CAEFRIID:IdentityDocuments/CAEFRIID:IdentityDocumentData">
                      <tr>
                        <td style="padding: 5px">
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000087'">
                              Лична карта
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000088'">
                              Паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000089'">
                              Дипломатически паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000090'">
                              Служебен паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000091'">
                              Моряшки паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000092'">
                              Военна карта за самоличност
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000093'">
                              Свидетелство за управление на моторно превозно средство
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000094'">
                              Временен паспорт
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000095'">
                              Служебен открит лист за преминаване на границата
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test="IDD:IdentityDocumentType = '0006-000096'">
                              Временен паспорт за окончателно напускане на Република България
                            </xsl:when>
                          </xsl:choose>
                        </td>
                        <td style="padding: 5px">
                          <xsl:value-of select="IDD:IdentityNumber" />
                        </td>
                        <td style="padding: 5px">
                          <xsl:value-of select="IDD:IdentityIssuer" />
                        </td>
                        <td style="padding: 5px">
                          <xsl:choose>
                            <xsl:when test="IDD:IdentitityIssueDate">
                              <xsl:value-of  select="ms:format-date(IDD:IdentitityIssueDate , 'dd.MM.yyyy')"/>&#160;г.
                            </xsl:when>
                          </xsl:choose>
                        </td>
                        <td style="padding: 5px">
                          <xsl:choose>
                            <xsl:when test="IDD:IdentitityExpireDate">
                              <xsl:value-of  select="ms:format-date(IDD:IdentitityExpireDate , 'dd.MM.yyyy')"/>&#160;г.
                            </xsl:when>
                          </xsl:choose>
                        </td>
                        <td style="padding: 5px">
                          <xsl:value-of select="IDD:IdentityDocumentStatus" />
                        </td>
                      </tr>
                    </xsl:for-each>
                  </table>
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

              Удостоверението да послужи пред &#160;
              <xsl:choose>
                <xsl:when test="CAEFRIID:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo">
                  <xsl:value-of  select="CAEFRIID:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of  select="CAEFRIID:DocumentMustServeTo/DMST:AbroadDocumentMustServeTo"/>
                </xsl:otherwise>
              </xsl:choose>.

            </td>
          </tr>
          <tr>
            <td colspan ="2">

              Данните в удостоверението са актуални към&#160;
              <xsl:choose>
                <xsl:when test="CAEFRIID:ReportDate">
                  <xsl:value-of select="xslExtension:FormatDate(CAEFRIID:ReportDate, 'dd.MM.yyyy')"/>&#160;г.&#160;
                  <xsl:value-of select="xslExtension:FormatDate(CAEFRIID:ReportDate, 'HH:mm:ss')"/>&#160;ч.&#160;
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
			 <td align="left" width="50%" style="vertical-align: top;">
				 Дата:
				 <xsl:choose>
					 <xsl:when test="CAEFRIID:DocumentReceiptOrSigningDate">
						 <xsl:value-of  select="ms:format-date(CAEFRIID:DocumentReceiptOrSigningDate , 'dd.MM.yyyy')"/>&#160;г.
					 </xsl:when>
				 </xsl:choose>
			 </td>
			 <td width="50%" style="vertical-align: top;">
				 <table>
					 <tbody style="vertical-align: top;">
					   <tr>
  						 <td rowspan="2">
  							 <span style="text-transform: uppercase;">
  								 <xsl:value-of select="CAEFRIID:Official/CAEFRIID:Position" />
  							 </span>
  						 </td>
  						 <td>&#160;</td>
  					 </tr>
  					 <tr>
  						 <td style="padding: 1rem 0 1rem 1rem">
  							 <xsl:value-of select="CAEFRIID:Official/CAEFRIID:PersonNames/NM:First/." />&#160;<xsl:value-of select="CAEFRIID:Official/CAEFRIID:PersonNames/NM:Last/." />
  						 </td>
  					 </tr>
  					 <tr>
  						 <td colspan="2">
  							 <xsl:call-template name="DocumentSignatures">
  								 <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
  							 </xsl:call-template>
  						 </td>
  					 </tr>
					 </tbody>
				 </table>
			 </td>
		 </tr>
        </table>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
