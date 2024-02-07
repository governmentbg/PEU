<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:template name="ShowDateInBoxes">
    <xsl:param name="DateTime"></xsl:param>
    <xsl:if test="$DateTime !=''">
      <div style="width:23px; float: left;" class="textbox">
        <xsl:call-template name="GetDatePart">
          <xsl:with-param name="DateTime" select="$DateTime"/>
          <xsl:with-param name="DatePart" select="'dd'"/>
        </xsl:call-template>
      </div>
      <div style="width:23px; float: left; margin-left:4px;" class="textbox">
        <xsl:call-template name="GetDatePart">
          <xsl:with-param name="DateTime" select="$DateTime"/>
          <xsl:with-param name="DatePart" select="'MM'"/>
        </xsl:call-template>
      </div>
      <div style="width:45px; float: left; margin-left:4px; " class="textbox">
        <xsl:call-template name="GetDatePart">
          <xsl:with-param name="DateTime" select="$DateTime"/>
          <xsl:with-param name="DatePart" select="'yyyy'"/>
        </xsl:call-template>
      </div>
    </xsl:if>
    <xsl:if test="$DateTime =''">
      <div style="width:23px; float: left;" class="textbox">
      </div>
      <div style="width:23px; float: left; margin-left:4px;" class="textbox">
      </div>
      <div style="width:45px; float: left; margin-left:4px; " class="textbox">
      </div>
    </xsl:if>
  </xsl:template>

  <xsl:template name="GetDatePart">
    <xsl:param name="DateTime"/>
    <xsl:param name="DatePart"/>
    <xsl:choose>
      <xsl:when test="$DatePart='dd'">
        <xsl:value-of select="substring($DateTime,9,2)"/>
      </xsl:when>
      <xsl:when test="$DatePart='MM'">
        <xsl:value-of select="substring($DateTime,6,2)"/>
      </xsl:when>
      <xsl:when test="$DatePart='yyyy'">
        <xsl:value-of select="substring($DateTime,1,4)"/>
      </xsl:when>
    </xsl:choose>
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
      <xsl:value-of select="$day"/>
      <xsl:value-of select="'.'"/>
      <xsl:value-of select="$month"/>
      <xsl:value-of select="'.'"/>
      <xsl:value-of select="$year"/>
      <xsl:value-of select="' '"/>
      <xsl:value-of select="$hour"/>
    </span>
  </xsl:template>

  <xsl:template name="FormatCurrency">
    <xsl:param name="Amount"/>
    <xsl:value-of select='format-number($Amount, "#.00")'/>
  </xsl:template>

  <xsl:template name ="ShowIdentifier">
    <xsl:for-each select="*[local-name()='ElectronicServiceApplicant']">
      <xsl:for-each select="*[local-name()='RecipientGroup']">
        <xsl:for-each select="*[local-name()='Recipient']">
          <xsl:if test="*[local-name()='Person']">
            <xsl:for-each select="*[local-name()='Person']">
              <xsl:for-each select="*[local-name()='Identifier']">
                <xsl:if test="*[local-name()='EGN']">
                  <span class="dataLabel">
                    <xsl:value-of select="."/>
                  </span>
                </xsl:if>
                <xsl:if test="*[local-name()='LNCh']">
                  <span class="dataLabel">
                    <xsl:value-of select="."/>
                  </span>
                </xsl:if>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:if>
          <xsl:if test="*[local-name()='Entity']">
            <xsl:for-each select="*[local-name()='Entity']">
              <xsl:for-each select="*[local-name()='Identifier']">
                <span class="dataLabel" >
                  <xsl:value-of select="."/>
                </span>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:if>
        </xsl:for-each>
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="ShowElectronicServiceApplicantNames">
    <xsl:variable name="space">
      <xsl:value-of select="' '"></xsl:value-of>
    </xsl:variable>
    <xsl:for-each select="*[local-name()='ElectronicServiceApplicant']">
      <xsl:for-each select="*[local-name()='RecipientGroup']">
        <xsl:for-each select="*[local-name()='Recipient']">
          <xsl:if test="*[local-name()='Person']">
            <xsl:for-each select="*[local-name()='Person']">
              <xsl:for-each select="*[local-name()='Names']">
                <span class="dataLabel" >
                  <xsl:value-of select="*[local-name()='First']/."/>
                  <xsl:value-of select="$space"/>
                  <xsl:value-of select="*[local-name()='Middle']/."/>
                  <xsl:value-of select="$space"/>
                  <xsl:value-of select="*[local-name()='Last']/."/>
                </span>
              </xsl:for-each>
            </xsl:for-each>
          </xsl:if>
          <xsl:if test="*[local-name()='Entity']">
            <xsl:for-each select="*[local-name()='Entity']">
              <span class="dataLabel" >
                <xsl:value-of select="*[local-name()='Name']/."/>
              </span>
            </xsl:for-each>
          </xsl:if>
        </xsl:for-each>
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="ShowDocumentURI">
    <xsl:param name="RegisterIndex"/>
    <xsl:param name="SequenceNumber"/>
    <xsl:param name="ReceiptOrSigningDate"/>
    <span class="dataLabel">
      <xsl:value-of select="$RegisterIndex"/>-<xsl:value-of select="$SequenceNumber"/>-<xsl:call-template name="FormatDate">
        <xsl:with-param name="DateTime" select="$ReceiptOrSigningDate"/>
      </xsl:call-template>
    </span>
  </xsl:template>

  <xsl:template name="ShowAISCaseURI">
    <xsl:for-each select="*[local-name()='AISCaseURI']">
      <xsl:if test="*[local-name()='DocumentInInternalRegisterURI']">
        <span class="dataLabel">
          <xsl:value-of select="."/>
        </span>
      </xsl:if>
      <xsl:if test="*[local-name()='DocumentURI']">
        <xsl:for-each select="*[local-name()='DocumentURI']">
          <xsl:call-template name="ShowDocumentURI">
            <xsl:with-param name="RegisterIndex" select="*[local-name()='RegisterIndex']/."></xsl:with-param>
            <xsl:with-param name="SequenceNumber" select="*[local-name()='SequenceNumber']/."></xsl:with-param>
            <xsl:with-param name="ReceiptOrSigningDate" select="*[local-name()='ReceiptOrSigningDate']/."></xsl:with-param>
          </xsl:call-template>
        </xsl:for-each>
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>
