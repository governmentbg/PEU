<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:RAM="http://ereg.egov.bg/segment/0009-000019"
               xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                 xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                 xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                 xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
                xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
                xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
                 xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="urn:schemas-microsoft-com:xslt"
                xsi:type="xsl:transform">
  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="RAM:ReceiptAcknowledgedMessage">
    <html>
      <body>
        <table align="center" cellpadding="5" width= "700px" >
          <tr>
            <td colspan="2">
              <div class="print-document flex-container">
                <div class="document-section document-header">
                  <center>
                    <h1 class="text-center document-title">Потвърждаване за получаване</h1>
                  </center>
                </div>
                <div class="document-section">
                  <p>
                    Подаден документ: <xsl:value-of select="RAM:DocumentTypeName" />
                  </p>
                  <p>
                    УРИ на регистриран документ: <xsl:value-of select="RAM:DocumentURI/DU:RegisterIndex" />
                    -<xsl:value-of select="RAM:DocumentURI/DU:SequenceNumber" />
                    -<xsl:value-of select="ms:format-date(RAM:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                  </p>
                  <p>
                    Време на получаване: <xsl:value-of select="ms:format-date(RAM:ReceiptTime, 'dd.MM.yyyy hh:mm:ss')"/>
                  </p>
                  <p>
                    Вид на пренос:
                    <xsl:choose>
                      <xsl:when test="RAM:TransportType = '0006-000001'">
                        Пренос на електронен документ чрез уеб базирано приложение
                      </xsl:when>
                      <xsl:when test="RAM:TransportType = '0006-000002'">
                        Пренос на електронен документ чрез електронна поща
                      </xsl:when>
                      <xsl:when test="RAM:TransportType = '0006-000003'">
                        Пренос на електронен документ чрез физически носител
                      </xsl:when>
                      <xsl:when test="RAM:TransportType = '0006-000004'">
                        Пренос на електронен документ чрез единната среда за обмен на електронни документи
                      </xsl:when>
                    </xsl:choose>
                  </p>
                </div>
                <div class="document-section">
                  <p>
                    Заявител на електронната административна услуга:
                    <xsl:call-template name="PersonName">
                      <xsl:with-param name="Names" select = "RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
                    </xsl:call-template>
                  </p>
                  <p>
                    <xsl:call-template name="PersonIdentifier">
                      <xsl:with-param name="Identifier" select = "RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier" />
                    </xsl:call-template>
                    <br></br>
                    <xsl:call-template name="IdentityDocument">
                      <xsl:with-param name="Document" select = "RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
                    </xsl:call-template>
                  </p>
                  <p>
                    Качество на заявителя:
                    <xsl:choose>
                      <xsl:when test="RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1001'">
                        лично качество (за собствени нужди)
                      </xsl:when>
                      <xsl:when test="RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002'">
                        пълномощник на друго физическо или юридическо лице
                      </xsl:when>
                      <xsl:when test="RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1003'">
                        законен представител на юридическо лице
                      </xsl:when>
                      <xsl:when test="RAM:Applicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1004'">
                        длъжностно лице
                      </xsl:when>
                    </xsl:choose>
                  </p>
                  <p>
                    Получател на електронната административна услуга:
                    <xsl:if test="RAM:Applicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                      <xsl:call-template name="PersonBasicData" >
                        <xsl:with-param name="Person" select = "RAM:Applicant/ESA:RecipientGroup/ESA:Recipient/REC:Person" />
                      </xsl:call-template>
                    </xsl:if>
                    <xsl:if test="RAM:Applicant/ESA:RecipientGroup/ESA:Recipient/REC:ForeignPerson">
                      <xsl:call-template name="ForeignCitizenBasicData" />
                    </xsl:if>
                    <xsl:if test="RAM:Applicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                      <xsl:call-template name="EntityBasicData" >
                        <xsl:with-param name="Entity" select = "RAM:Applicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity" />
                      </xsl:call-template>
                    </xsl:if>
                    <xsl:if test="RAM:Applicant/ESA:RecipientGroup/ESA:Recipient/REC:ForeignEntity">
                      <xsl:call-template name="ForeignEntityBasicData" />
                    </xsl:if>
                  </p>
                </div>
              </div>
            </td>
          </tr>
        </table>

      </body>
    </html>
  </xsl:template>

  <xsl:template name="PersonName">
    <xsl:param name="Names"/>
    <xsl:value-of  select="$Names/NM:First/."/>&#160;<xsl:choose>
      <xsl:when test="$Names/NM:Middle/.">
        <xsl:value-of  select="$Names/NM:Middle/."/>&#160;
      </xsl:when>
    </xsl:choose><xsl:value-of  select="$Names/NM:Last/."/>
  </xsl:template>
  <!-- 0009-000006 - PersonIdentifier -->
  <xsl:template name="PersonIdentifier">
    <xsl:param name="Identifier"/>
    <xsl:if test="$Identifier/PI:EGN">
      ЕГН:
    </xsl:if>
    <xsl:if test="$Identifier/PI:LNCh">
      ЛНЧ:
    </xsl:if>
    <span class="dataLabel">
      <xsl:value-of select="$Identifier/."/>
    </span>

  </xsl:template>
  <!-- 0009-000008 - PersonBasicData -->
  <xsl:template name="PersonBasicData">
    <xsl:param name="Person"/>
    <xsl:call-template name="PersonName">
      <xsl:with-param name="Names" select = "$Person/P:Names" />
    </xsl:call-template>
    <br></br>
    <xsl:call-template name="PersonIdentifier">
      <xsl:with-param name="Identifier" select = "$Person/P:Identifier" />
    </xsl:call-template>
    <br></br>
    <xsl:call-template name="IdentityDocument">
      <xsl:with-param name="Document" select = "$Person/P:IdentityDocument" />
    </xsl:call-template>
  </xsl:template>

  <!-- 0009-000011 - ForeignCitizenBasicData -->
  <xsl:template name="ForeignCitizenBasicData">

  </xsl:template>

  <!-- 0009-000013 - EntityBasicData -->
  <xsl:template name="EntityBasicData">
    <xsl:param name="Entity"/>
    <p>
      <xsl:value-of select="$Entity/EBD:Name/."/>
    </p>
    <p>
      ЕИК/БУЛСТАТ:
      <xsl:value-of select="$Entity/EBD:Identifier/."/>
    </p>
  </xsl:template>

  <!-- 0009-000014 - ForeignEntityBasicData -->
  <xsl:template name="ForeignEntityBasicData">

  </xsl:template>

  <!-- 0009-000099 - IdentityDocument -->
  <xsl:template name="IdentityDocument">
    <xsl:param name="Document"/>

    <xsl:choose>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000087'">
        Лична карта
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000088'">
        Паспорт
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000089'">
        Дипломатически паспорт
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000115'">
        Дипломатически паспорт
      </xsl:when>      
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000090'">
        Служебен паспорт
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000091'">
        Моряшки паспорт
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000092'">
        Военна карта за самоличност
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000093'">
        Свидетелство за управление на моторно превозно средство
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000094'">
        Временен паспорт
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000095'">
        Служебен открит лист за преминаване на границата
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000096'">
        Временен паспорт за окончателно напускане на Република България
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000097'">
        Карта на бежанец
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000098'">
        Карта на чужденец, получил убежище
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000099'">
        Карта на чужденец с хуманитарен статус
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = '0006-000122'">
        Удостоверение за пребиваване на гражданин на ЕС
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = ' 0006-000121'">
        Разрешение за пребиваване
      </xsl:when>
      <xsl:when test="$Document/IDBD:IdentityDocumentType = ' 0006-000120'">
        Удостоверение за пребиваване на гражданин на ЕС
      </xsl:when>
      <xsl:otherwise>
        Документ
      </xsl:otherwise>
    </xsl:choose>
    №: <xsl:value-of select="$Document/IDBD:IdentityNumber" />  <br></br>
    изд. на: <xsl:value-of  select="ms:format-date($Document/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.
    от <xsl:value-of select="$Document/IDBD:IdentityIssuer" />
  </xsl:template>
</xsl:stylesheet>
