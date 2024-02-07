<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:RII="http://ereg.egov.bg/segment/R-3010"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension"
                xsi:type="xsl:transform" >

  <xsl:include href="./BaseTemplates.xslt"/>
  <xsl:include href="./SignatureBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:param name="ApplicationPath"></xsl:param>
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

</xsl:stylesheet>