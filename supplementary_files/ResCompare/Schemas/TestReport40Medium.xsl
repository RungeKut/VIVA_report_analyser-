<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>
  <xsl:template name="getFilename">
    <xsl:param name="filePath" />
    <xsl:variable name="rest-of" select="substring-after($filePath, '\')" />
    <xsl:choose>
        <xsl:when test="contains($rest-of, '\')">
            <xsl:call-template name="getFilename">
                <xsl:with-param name="filePath" select="$rest-of" />
            </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
            <xsl:value-of select="$rest-of" />
        </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template match="R">

    <HTML>
      <HEAD>
        <TITLE>Test Report Medium</TITLE>
      </HEAD>

      <BODY
       oncontextmenu="return false"
       background=".\TestReport.jpg"
       onload="if ( parent.adjustIFrameSize ) parent.adjustIFrameSize( window ) ;" >

        <H3>
          <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>Test Report
        </H3>

        <TABLE border="0">
          <TR>
            <TD>
              <I>Board Name:</I>
            </TD>
            <TD>
              <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ST/@NM"/>
              </B>
            </TD>
          </TR>
          <TR>
            <TD>
              <I>Board Code:</I>
            </TD>
            <TD>
              <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ST/@BC"/>
              </B>
            </TD>
          </TR>
          <TR>
            <TD>
              <I>Operator:</I>
            </TD>
            <TD>
              <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ST/@OP"/>
              </B>
            </TD>
          </TR>
          <TR>
            <TD>
              <I> Test Method:&#160;&#160; </I>
            </TD>
            <TD>
              <B>
                <xsl:value-of select="ST/@TS"/>
              </B>
            </TD>
            <BR></BR>
          </TR>
          <TR>
            <TD>
              <I>Date:</I>
            </TD>
            <TD>
              <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ST/@SD"/>
              </B>
            </TD>
          </TR>
        </TABLE>

        <html>
          <xsl:choose>
            <xsl:when test="ST/@KR = 0 ">
              Report Type: Only failed tested.
            </xsl:when>

            <xsl:when test="ST/@KR = 1 ">
              Report Type: All component tested.
            </xsl:when>

            <xsl:otherwise>

            </xsl:otherwise>
          </xsl:choose>

          <xsl:variable  name = "MaxPrtErr" >
               <xsl:choose>
                    <xsl:when  test = "ST/@ME">
                         <xsl:value-of select = "ST/@ME"/>
                    </xsl:when>
                    <xsl:otherwise>
                         0
                    </xsl:otherwise>
               </xsl:choose>
          </xsl:variable>

          <xsl:variable  name = "ShowImage" >
               <xsl:choose>
                    <xsl:when  test = "ST/@SI">
                         <xsl:value-of select = "ST/@SI"/>
                    </xsl:when>
                    <xsl:otherwise>
                         0
                    </xsl:otherwise>
               </xsl:choose>
          </xsl:variable>

          <!--   <xsl:value-of select="$MaxPrtErr" /> -->

        <xsl:for-each select = "ET">
               <!-- <MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
               <xsl:for-each select = "WARNING">
                    <TR>
                    <TD>
                              <font size="-1" color="TOMATO">
                                   <xsl:if test="@MaxTestedFailBoards != 0">
                                        <xsl:text>&#160; ( Warning Maximum Board Errors : &#160;</xsl:text>
                                        <B> <xsl:value-of select="@MaxTestedFailBoards"/> </B> <xsl:text>&#160;)</xsl:text>
                                        <BR></BR>
                                        <xsl:text>&#160; ( Warning Panel Not Complete Tested ) &#160;</xsl:text>
                                        <BR></BR>
                                   </xsl:if>
                              </font>
                         </TD>
                    </TR>
               </xsl:for-each>
               <!-- </MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
          </xsl:for-each>

          <xsl:for-each select = "BI">
           <xsl:if test="count(TEST) > 0">

            <BR></BR>
            <xsl:text>&#160;</xsl:text>

            <font color="BLUE">
              Bd: <xsl:text>&#160;</xsl:text> <B>
                <xsl:value-of select="@ID+1"/>
              </B>
            </font>

            <xsl:if test="@BC != ''">
              <xsl:text>&#160;&#160;</xsl:text>
              <font color="BLUE">
                Code: <xsl:text>&#160;</xsl:text> <B>
                  <xsl:value-of select="@BC"/>
                </B>
              </font>
              <BR></BR>
              <xsl:text>&#160;&#160;&#160;&#160;</xsl:text>
            </xsl:if>

            <xsl:text>&#160;</xsl:text>
            <font color="BLUE">
              Tests: <xsl:text>&#160;</xsl:text> <B>
                <xsl:value-of select="@NT"/>
              </B>
            </font>

            <xsl:text>&#160;</xsl:text>
            <font color="BLUE">
              Errors: <xsl:text>&#160;</xsl:text> <B>
                <xsl:value-of select="@NF"/>
              </B>
            </font>
            <BR></BR>
            <BR></BR>
			
			<xsl:if test="@AK = 17 and @TR=2">
				<xsl:text>&#160;</xsl:text>                    
					<FONT COLOR="red">
					Bad Mark found or board not present.
					</FONT>
				<BR></BR>   
		    </xsl:if>

            <TABLE>
              <xsl:for-each select = "TEST">

                <xsl:variable  name = "PrintedErr"  select = "position()"/>

               <!--   <xsl:value-of select="MaxPrtErr" />   -->
               <!--   <xsl:value-of select="$PrintedErr" />   -->

                <xsl:if test="$PrintedErr &lt;= $MaxPrtErr or $MaxPrtErr = 0">

                        <TR>

                          <TD>
                            <xsl:text>&#160;&#160;</xsl:text>
                            <xsl:value-of select="@NM"/>
                            <xsl:text>&#160;&#160;</xsl:text>
                          </TD>

                          <TD>
                            <xsl:text>&#160;</xsl:text>

                            <xsl:if test="@TR > 0 and @MU != 'E'">
                              <font color="RED">
                                <I> Fail:</I>
                                <xsl:text>&#160;</xsl:text>
                                <B>
                                  <xsl:value-of select="@MP"/>
                                </B>
                              </font>
                            </xsl:if>
                            <!--   <xsl:3.08.2011lau regolaPeter: scrivere solo Fail o solo Pass se Unit = E" />   -->
                            <xsl:if test="@TR > 0 and @MU = 'E'">
                              <font color="RED">
                                <I> Fail </I>
                                <xsl:text>&#160;</xsl:text>
                              </font>
                            </xsl:if>

                            <xsl:if test="@TR = 0 and @MU != 'E'">
                              <font color="GREEN">
                                <B>
                                  Pass: <xsl:value-of select="@MP"/>
                                </B>
                              </font>
                            </xsl:if>

                            <xsl:if test="@TR = 0 and @MU = 'E'">
                              <font color="GREEN">
                                <B>
                                  Pass
                                </B>
                              </font>
                            </xsl:if>

                            <xsl:if test="@TR = -1 and @MU != 'E'">
                              <font color="GRAY">
                                <B>
                                  Skip: <xsl:value-of select="@MP"/>
                                </B>
                              </font>
                            </xsl:if>
                            <xsl:if test="@TR = -1 and @MU = 'E'">
                              <font color="GRAY">
                                <B>
                                  Skip
                                </B>
                              </font>
                            </xsl:if>

                            <xsl:text>&#160;</xsl:text>
                          </TD>

                          <xsl:if test="@MU != 'E'">
                          <TD align="left">
                            <xsl:choose>
                              <xsl:when test="@MR = '1.79769e+308'">
                                Overflow
                              </xsl:when>
                              <xsl:when test="@MR = '1.79769e-308'">
                                Underflow
                              </xsl:when>
                              <xsl:otherwise>
                              <xsl:value-of select="@MR"/>
                              <xsl:text>&#160;</xsl:text>
                              <xsl:value-of select="@MU"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </TD>
                          <TR>
                            <TD>
                              <xsl:text>&#160;&#160;</xsl:text>
                              <xsl:value-of select="@SC"/>
                            </TD>
                          </TR>
                          </xsl:if>
                          <xsl:if test="@IN != ''">
                            <TR>
                              <TD>
                                <PRE>
                                  <xsl:text>&#160;&#160;</xsl:text>
                                  <xsl:value-of select="@IN"/>
                                </PRE>
                              </TD>
                            </TR>
                          </xsl:if>
                          <xsl:if test="@FR != ''">
                            <TR>
                              <TD>
                                <PRE>
                                  <xsl:text>&#160;&#160;</xsl:text>
                                  <!-- Invertire l'abilitazione delle due linee seguenti per disabilitare la gestione colori o altro da utente -->
                                   <xsl:value-of disable-output-escaping="yes" select ="@FR" />  -->
                                  <!-- <xsl:value-of select="@FR"/> -->
                                </PRE>
                              </TD>
                            </TR>
                          </xsl:if>
                          <xsl:if test="@PIC != '' and $ShowImage != 0">
                            <TR>
                              <TD>
                                <PRE>
                                  <img width="320" height="240" alt="{@PIC}">
                                    <xsl:attribute name="src">
                                      <xsl:call-template name="getFilename">
                                          <xsl:with-param name="filePath" select="@PIC" />
                                      </xsl:call-template>
                                    </xsl:attribute>
                                  </img>
                                </PRE>
                              </TD>
                            </TR>
                          </xsl:if>

                          <!-- <xsl:if test="@F = 'FNode' or @F = 'Fan' or @F = 'Contact' or @F = 'Pan' or @F = 'Isolation' "> -->
                 <xsl:variable  name = "TestName"  select = "translate(@F,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVXYZ')"/>
                          <xsl:if test=" $TestName =  'FNODE' or $TestName = 'FAN' or $TestName = 'CONTACT' or $TestName = 'PAN' or $TestName = 'ISOLATION' or $TestName = 'SHORT' or $TestName = 'CONTINUITY' ">
                            <TR>
                              <TD>
                                <xsl:text>&#160; Pin1: &#160;&#160; </xsl:text>
                                <xsl:value-of select="@CP1"/>
                                <br/>
                                <xsl:text>&#160; Pin2: &#160;&#160; </xsl:text>
                                <xsl:value-of select="@CP2"/>
                              </TD>
                            </TR>
                          </xsl:if>

                          <!-- 110713gs gestione del campo diagnostica istruzioni -->
                          <xsl:if test="@DG > 0">
                            <TR>
                              <TD>
                                <xsl:if test="@DG = 1">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Missing Component</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 2">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Short on the Component</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 3">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Component out of tolerance</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 4">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Component out of positive tolerance</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 5">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Component out of negative tolerance</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 6">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Component out of positive tolerance or missing guarding</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 7">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Component out of negative tolerance or missing guarding</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 8">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Component reversed</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 9">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Pin not soldered</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 10">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Pin shorted to GND</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 11">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Pin shorted to VCC</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 12">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Pin shorted</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 13">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Bad Component</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 14">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Bad Component ( State On Fail )</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                                <xsl:if test="@DG = 15">
                                  <font size="2">
                                    <font face="Verdana">
                                      <xsl:text>&#160;&#160;-- Bad Component ( State Off Fail )</xsl:text>
                                    </font>
                                  </font>
                                </xsl:if>
                              </TD>
                            </TR>
                          </xsl:if>

                          <!-- 110713gs FINE gestione del campo diagnostica istruzioni -->
                        </TR>

                  </xsl:if>
              </xsl:for-each>

     <!-- <MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
              <xsl:for-each select = "WARNING">
                <TR>
                  <TD>
                    <font size="-1" color="TOMATO">
                        <xsl:if test="@MaxPrintedErrors != 0">
                          <xsl:text>&#160; ( Warning Maximum Printed Errors : &#160;</xsl:text>
                          <B>
                            <xsl:value-of select="@MaxPrintedErrors"/>
                          </B>
                          <xsl:text>&#160;)</xsl:text>
                        </xsl:if>
                        <xsl:if test="@MaxWrittenErrors != 0">
                          <xsl:text>&#160; ( Warning Maximum Written Errors : &#160;</xsl:text>
                          <B>
                            <xsl:value-of select="@MaxWrittenErrors"/>
                          </B>
                          <xsl:text>&#160;)</xsl:text>
                        </xsl:if>
                        <xsl:if test="@MaxTestedErrors != 0">
                          <xsl:text>&#160; ( Warning Maximum Tested Errors : &#160;</xsl:text>
                          <B>
                            <xsl:value-of select="@MaxTestedErrors"/>
                          </B>
                          <xsl:text>&#160;)</xsl:text>
                        </xsl:if>
                    </font>
                  </TD>
                </TR>
              </xsl:for-each>
     <!-- </MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->

            </TABLE>

           </xsl:if>
          </xsl:for-each>

        </html>

        <br></br>
        <xsl:choose>
          <xsl:when test="ET/@NF != 0 ">
            <!--    <font color="RED">    -->
            <H4>
              <xsl:text>&#160;</xsl:text> Total Tests:  <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ET/@NT"/>   -
              </B>
              Error(s): <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ET/@NF"/>
              </B>
            </H4>
            <!--      <BR></BR>   -->
            <H3>
              <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
              Test Failed
            </H3>
            <!--     </font>              -->
          </xsl:when>

          <xsl:when test="ET/@NT != 0 ">
            <!--    <font color="GREEN">  -->
            <H4>
              <xsl:text>&#160;</xsl:text> Total Tests:  <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ET/@NT"/>   -
              </B>
              Error(s): <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ET/@NF"/>
              </B>
            </H4>
            <!--      <BR></BR>   -->
            <H3>
              <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
              Test Passed
            </H3>
            <!--    </font>               -->
          </xsl:when>

          <xsl:otherwise>
            <!--    <font color="BLUE">   -->
            <H4>
              <xsl:text>&#160;</xsl:text> Total Tests:  <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ET/@NT"/>   -
              </B>
              Error(s): <xsl:text>&#160;</xsl:text>
              <B>
                <xsl:value-of select="ET/@NF"/>
              </B>
            </H4>
            <!--      <BR></BR>   -->
            <H3>
              <font color="RED">
                <xsl:choose>
                  <xsl:when test="ET/@AK = 0 ">
                  </xsl:when>

                  <xsl:when test="ET/@AK = 1 ">
                    <xsl:text>&#160;&#160;&#160;</xsl:text>
                    General Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 2 ">
                    <xsl:text>&#160;&#160;</xsl:text>
                    Abort by Operator.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 3 ">
                    <xsl:text>&#160;&#160;</xsl:text>
                    Markers Not Found.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 4 ">
                    <xsl:text>&#160;</xsl:text>
                    Z-Markers Not Found.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 5 ">
                    <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
                    COM Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 6 ">
                    <xsl:text>&#160;</xsl:text>
                    Data Base Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 7 ">
                    <xsl:text>&#160;</xsl:text>
                    Motion Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 8 ">
                    <xsl:text>&#160;</xsl:text>
                    Binary File Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 9 ">
                    <xsl:text>&#160;</xsl:text>
                    System ATE is OFF.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 10 ">
                    <xsl:text>&#160;</xsl:text>
                    System ATE is ON but can't initialize it.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 11 ">
                    <xsl:text>&#160;</xsl:text>
                    Test Motion Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 12 ">
                    <xsl:text>&#160;</xsl:text>
                    Too Many Tested Errors.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 13 ">
                    <xsl:text>&#160;</xsl:text>
                    Barcode Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 14 ">
                    <xsl:text>&#160;</xsl:text>
                    RFID Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 15 ">
                    <xsl:text>&#160;</xsl:text>
                    Diversity Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>

                  <xsl:when test="ET/@AK = 18 ">
                    <xsl:text>&#160;</xsl:text>
                    Temperature Out Of Range.
                    <BR></BR>   <BR></BR>
                  </xsl:when>
                  
                  <xsl:when test="ET/@AK = 20 ">
                    <xsl:text>&#160;</xsl:text>
                    Stamper Error.
                    <BR></BR>   <BR></BR>
                  </xsl:when>
                  
                  <xsl:otherwise>
                    <xsl:text>&#160;</xsl:text>
                    NOT Coded Error.
                    <BR></BR>   <BR></BR>
                  </xsl:otherwise>
                </xsl:choose>
              </font>
              <font color="BLACK">
                <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
                Test Skipped !!!
              </font>
            </H3>
            <!--    </font>
                              -->
          </xsl:otherwise>
        </xsl:choose>

      </BODY>

    </HTML>

  </xsl:template>

</xsl:stylesheet>
