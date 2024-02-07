<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:RII="http://ereg.egov.bg/segment/R-3010"
                              xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
                              xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
                              xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                              xmlns:RDU="http://ereg.egov.bg/segment/0009-000044"
                              xmlns:FCN="http://ereg.egov.bg/segment/0009-000007"
                              xmlns:PN="http://ereg.egov.bg/segment/0009-000005"
                              xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                              xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                              xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:xslExtension="urn:XSLExtension"
                              xsi:type="xsl:transform" >


  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="RII:RemovingIrregularitiesInstructions">
    <html>
      <xsl:call-template name="Head"></xsl:call-template>
      <body>
        <xsl:variable name="space">
          <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
        </xsl:variable>
        <div id="content">
          <table class="mainTable" cellspacing="0" cellpadding="0">
            <tr>
              <td class="mainCell" colspan="2" align="center">
                <h4>
                  <xsl:for-each select="RII:ElectronicServiceProviderBasicData">
                    <xsl:call-template name="ElectronicServiceProviderBasicData"/>
                  </xsl:for-each>
                  <br></br>
                  <br/>
                  <xsl:value-of select="RII:RemovingIrregularitiesInstructionsHeader/."/>
                </h4>
                Рег. №:
                <b>
                  <xsl:for-each select="RII:IrregularityDocumentURI">
                    <xsl:call-template name="DocumentURI" />
                  </xsl:for-each>
                </b>
                от дата
                <b>
                  <xsl:call-template name="FormatDate">
                    <xsl:with-param name="DateTime" select="RII:IrregularityDocumentReceiptOrSigningDate/."/>
                  </xsl:call-template>
                </b>

                <br></br>
                <br></br>
              </td>
            </tr>
            <tr>
              <td colspan ="2" class="mainCell">
                <div align="center" style="font-weight:bold;"> Уважаема госпожо/Уважаеми господине,</div>
                <br></br>
                <xsl:value-of select="$space"/>При проверка за редовност и допустимост на заявление с входящ №
                <xsl:for-each select="RII:ApplicationDocumentURI">
                  <xsl:call-template name="DocumentURI" />
                </xsl:for-each>
                <xsl:if test="RII:ApplicationDocumentReceiptOrSigningDate/.">
                  от дата
                  <xsl:call-template name="FormatDate">
                    <xsl:with-param name="DateTime" select="RII:ApplicationDocumentReceiptOrSigningDate/."/>
                  </xsl:call-template>
                </xsl:if>, по което е образувана преписка с №
                <xsl:for-each select="RII:AISCaseURI">
                  <xsl:call-template name="RegisteredDocumentURI" />
                </xsl:for-each>
                , са установени следните нередовности: <br></br> <br></br>
                <xsl:call-template name="Irregularities"></xsl:call-template>
                <br></br>
                <xsl:value-of select="$space"/>Срокът за отстраняване на нередовностите е

                <xsl:variable name="deadlineDays" select="xslExtension:ExtractFirstGroup(RII:DeadlineCorrectionIrregularities//., '(\d*)D')"/>
                <xsl:if test="$deadlineDays and $deadlineDays != '0'">
                  <xsl:value-of select="$deadlineDays"/>
                  <xsl:value-of select="' '" />
                  <xsl:if test="$deadlineDays = '1'">
                    <xsl:value-of select="'ден'"/>
                  </xsl:if>
                  <xsl:if test="$deadlineDays != '1'">
                    <xsl:value-of select="'дни'"/>
                  </xsl:if>.
                </xsl:if>

                <xsl:variable name="deadlineHours" select="xslExtension:ExtractFirstGroup(RII:DeadlineCorrectionIrregularities//., '(\d*)H')"/>
                <xsl:if test="$deadlineHours and $deadlineHours != '0'">
                  <xsl:value-of select="$deadlineHours"/>
                  <xsl:value-of select="' '" />
                  <xsl:value-of select="'час/а.'"/>
                </xsl:if>

                <br></br><br></br>
                <xsl:value-of select="$space"/>В случай, че нередовностите не бъдат отстранени в посочения срок, заявлението ще бъде прекратено служебно.
                <br></br>
                <br></br>
              </td>
            </tr>
            <tr>
              <td colspan="2" class="mainCell"  align="right">
                <br></br>
                <br></br>
                <xsl:value-of select="$space"/>Длъжностно лице:
                <xsl:for-each select="RII:Official">
                  <xsl:call-template name="Official" >
                    <xsl:with-param name="docNamespaceItem" select="." />
                  </xsl:call-template>
                </xsl:for-each>
              </td>
            </tr>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>


  <xsl:template name="Head">

    <head>
      <style>
        body, table
        {
        font-family: Verdana;
        font-size: 10pt;
        color: black;
        text-align: justify;
        }
        h1
        {
        font-size: 12pt;
        color: black;
        text-align: center;
        }
        h2
        {
        padding-top:5px;
        text-align:center;
        vertical-align:middle;
        font-size: 10pt;
        }
        h3
        {
        font-variant: small-caps;
        text-align:center;
        }
        h4
        {
        font-size: 12pt;
        font-variant: small-caps;
        text-align: center;
        }
        hr
        {
        border: 1px solid #000000;
        }
        .mainCell
        {
        vertical-align:top;
        }
        .mainTable
        {
        vertical-align:top;
        }
        .dataLabel
        {
        }
        .datalabel2 {
        color: #0000CC;
        }
        .literal
        {
        font-weight: normal;
        }
        .left
        {
        font-size: 8pt;
        padding: 3px 5px 3px 5px;
        vertical-align: top;
        width: 90px;
        text-align: left;
        }
        .right
        {
        font-size: 8pt;
        padding: 3px 5px 3px 5px;
        vertical-align: top;
        }
        .signTable
        {
        border: 1px solid #C0C0C0;
        padding: 0px 5px 5px 5px;
        vertical-align: top;
        font-size: 8pt;
        }
        .mini
        {
        font-family: Verdana;
        font-size: 8pt;
        font-variant: normal;
        font-weight:normal;
        }
        .header
        {
        background-color: #F0F0F0;
        color: #333333;
        font-style: italic;
        }
        .left_mini
        {
        width: 100px;
        text-align: center;
        }
        .legend_gray
        {
        font-weight: bold;
        color: #676767;
        }
        .greeBorders
        {
        background-position: inherit;
        padding: 0px 5px 5px 5px;
        border-right: #b0b0b0 1px solid;
        border-top: #b0b0b0 1px solid;
        border-left: #b0b0b0 1px solid;
        border-bottom: #b0b0b0 1px solid;
        background-image: inherit;
        background-repeat: inherit;
        background-attachment: inherit;
        background-color: #FFFFFF;
        margin-top: 10px;
        }
        #content {
        margin: 15mm 15mm 10mm 25mm;
        }

        .uppercase{
        text-transform: uppercase;
        }
      </style>
    </head>
  </xsl:template>

  <!-- 0009-000002 - ElectronicServiceProviderBasicData -->
  <xsl:template name="ElectronicServiceProviderBasicData">
    <xsl:for-each select="ESPBD:EntityBasicData">
      <xsl:call-template name="EntityBasicData" />
    </xsl:for-each>
  </xsl:template>
 
  <!-- 0009-000013 - EntityBasicData -->
  <xsl:template name="EntityBasicData">
    <span class="dataLabel uppercase">
      <xsl:value-of select="EBD:Name/."/>
    </span>
  </xsl:template>

  <!-- 0009-000001 - DocumentURI -->
  <xsl:template name="DocumentURI">
    <xsl:value-of select="DU:RegisterIndex/." />-<xsl:value-of select="DU:SequenceNumber/." />-<xsl:call-template name="FormatDate">
      <xsl:with-param name="DateTime" select="DU:ReceiptOrSigningDate/." />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="FormatDate">
    <xsl:param name="DateTime"/>
    <xsl:variable name="year">
      <xsl:value-of select="substring($DateTime,1,4)"/>
    </xsl:variable>
    <xsl:variable name="day">
      <xsl:value-of select="substring($DateTime,9,2)"/>
    </xsl:variable>
    <xsl:variable name="month">
      <xsl:value-of select="substring($DateTime,6,2)"/>
    </xsl:variable>
    <xsl:variable name="hour">
      <xsl:value-of select="substring($DateTime,12,5)"/>
    </xsl:variable>
    <span class="dataLabel">
      <xsl:if test="$day and $month and $year">
        <xsl:value-of select="$day"/>
        <xsl:value-of select="'.'"/>
        <xsl:value-of select="$month"/>
        <xsl:value-of select="'.'"/>
        <xsl:value-of select="$year"/>
        <xsl:if test="$hour">
          <xsl:value-of select="' '"/>
          <xsl:value-of select="$hour"/>
        </xsl:if>
      </xsl:if>
    </span>
  </xsl:template>

  <!-- 0009-000044 - RegisteredDocumentURI -->
  <xsl:template name="RegisteredDocumentURI">
    <xsl:if test="RDU:DocumentInInternalRegisterURI">
      <xsl:for-each select="RDU:DocumentInInternalRegisterURI">
        <span class="dataLabel">
          <xsl:value-of select="."/>
        </span>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="RDU:DocumentURI">
      <xsl:for-each select="RDU:DocumentURI">
        <xsl:call-template name="DocumentURI" />
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <!-- Ukazaniq za otstranqvane na neredovnosti -->
  <xsl:template name ="Irregularities">
    <!--<table style="margin-left:20px;">
      <tr>
        <td colspan="2"></td>
      </tr>-->
    <ul>
    <xsl:for-each select="RII:Irregularities">
      <xsl:text></xsl:text>
      <li><xsl:value-of select="RII:IrregularityType/."/>
      <xsl:text> </xsl:text>
      <xsl:if test="RII:AdditionalInformationSpecifyingIrregularity/. != ''">
        &#160;&#160;&#160;&#160;&#160;<xsl:value-of select="RII:AdditionalInformationSpecifyingIrregularity/. " disable-output-escaping="yes" />
      </xsl:if>
      </li>
    </xsl:for-each>
    </ul>
    <!--</table>-->
  </xsl:template>

 <xsl:template name="Official">
    <xsl:param name="docNamespaceItem" />
    <xsl:variable name="space">
      <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
    </xsl:variable>
    <xsl:for-each select="*[local-name() = 'ForeignCitizenNames'][namespace-uri()=namespace-uri($docNamespaceItem)]">
      <xsl:value-of select="$space"></xsl:value-of>
      <xsl:call-template name="ForeignCitizenNames" />
    </xsl:for-each>
    <xsl:for-each select="*[local-name() = 'PersonNames'][namespace-uri()=namespace-uri($docNamespaceItem)]">
      <xsl:value-of select="$space"></xsl:value-of>
      <xsl:call-template name="PersonNames" />
    </xsl:for-each>
    <xsl:if test="*[local-name() = 'ElectronicDocumentAuthorQuality'][namespace-uri()=namespace-uri($docNamespaceItem)]/.">
      <br></br>
      <xsl:value-of select="$space"></xsl:value-of>В качеството на:
      <span style="padding-left:3px;">
        <xsl:value-of select="*[local-name() = 'ElectronicDocumentAuthorQuality'][namespace-uri()=namespace-uri($docNamespaceItem)]/."/>
      </span>
      <br></br>
    </xsl:if>
    <br></br>
  </xsl:template>

  <!-- 0009-000007 - ForeignCitizenNames -->
  <xsl:template name="ForeignCitizenNames">
    <xsl:variable name="hasCyrilicName">
      <xsl:value-of select="FCN:FirstCyrillic/."/>
    </xsl:variable>
    <xsl:if test="$hasCyrilicName!=''">
      <xsl:value-of select="FCN:FirstCyrillic/." />
      <xsl:value-of select="$space" />
      <xsl:value-of select="FCN:LastCyrillic/." />
      <xsl:value-of select="$space" />
      <xsl:value-of select="FCN:OtherCyrillic/." />

    </xsl:if>
    <xsl:variable name="hasLatinName">
      <xsl:value-of select="FCN:FirstLatin/."/>
    </xsl:variable>
    <xsl:if test="$hasCyrilicName!='' and  $hasLatinName!=''">
      (
    </xsl:if>
    <xsl:value-of select="FCN:FirstLatin/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="FCN:LastLatin/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="FCN:OtherLatin/." />

    <xsl:if test="$hasCyrilicName!='' and  $hasLatinName!=''">
      )
    </xsl:if>

  </xsl:template>

    <xsl:variable name="space">
    <xsl:value-of select="' '"></xsl:value-of>
  </xsl:variable>


  <!-- 0009-000005 - PersonNames -->
  <xsl:template name="PersonNames">

    <xsl:value-of select="PN:First/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="PN:Middle/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="PN:Last/." />

  </xsl:template>
</xsl:stylesheet>