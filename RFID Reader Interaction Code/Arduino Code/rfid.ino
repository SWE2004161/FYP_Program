#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         9          
#define SS_PIN          10         

MFRC522 mfrc522(SS_PIN, RST_PIN);  

MFRC522::MIFARE_Key key;

void setup() {
  Serial.begin(9600); 
  SPI.begin();      
  mfrc522.PCD_Init(); 

  for (byte i = 0; i < 6; i++) {
    key.keyByte[i] = 0xFF;
  }
}

void loop() {
  // Look for new cards
  if ( ! mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  // Select one of the cards
  if ( ! mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  byte buffer[64];
  byte size = sizeof(buffer);
  byte block;
  MFRC522::StatusCode status;

 
    // Read the next command
    String command = Serial.readStringUntil('\n');
    command.trim();

  if (command.startsWith("WRITE")) {
  int spaceIndex = command.indexOf(' ');
  if (spaceIndex > -1) {
    String dataString = command.substring(spaceIndex + 1);
    dataString.trim();

    // Fill the buffer with your data
    for (byte i = 0; i < dataString.length() && i < 64; i++) {
      buffer[i] = dataString.charAt(i);
    }
  
    // If dataString length < 64, fill the rest of the buffer with 0
    for (byte i = dataString.length(); i < 64; i++) {
      buffer[i] = 0;
    }

    // Write to blocks 4 to 7
    for (block = 4; block <= 7; block++) {
      // Authenticate
      status = mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, block, &key, &(mfrc522.uid));
      if (status != MFRC522::STATUS_OK) {
        Serial.print("Authentication failed: ");
        Serial.println(mfrc522.GetStatusCodeName(status));
        return;
      }

      // Write block
      byte offset = (block - 4) * 16;
      status = mfrc522.MIFARE_Write(block, &buffer[offset], 16);
      if (status != MFRC522::STATUS_OK) {
        Serial.print("Writing failed: ");
        Serial.println(mfrc522.GetStatusCodeName(status));
        return;
      }
    }
  }
}

    if (command.startsWith("READ")) {
      // Show UID on serial monitor
      Serial.print("UID tag :");
      String content= "";
      byte letter;
      for (byte i = 0; i < mfrc522.uid.size; i++) {
        Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " ");
        Serial.print(mfrc522.uid.uidByte[i], HEX);
        content.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
        content.concat(String(mfrc522.uid.uidByte[i], HEX));
      }
      Serial.println();

      for (byte block = 8; block <= 11; block++) {
      // Authenticate
      status = mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, block, &key, &(mfrc522.uid));
      if (status != MFRC522::STATUS_OK) {
        Serial.print("Authentication failed: ");
        Serial.println(mfrc522.GetStatusCodeName(status));
        return;
      }

      // Read block
      status = mfrc522.MIFARE_Read(block, buffer, &size);
      if (status != MFRC522::STATUS_OK) {
        Serial.print("Reading failed: ");
        Serial.println(mfrc522.GetStatusCodeName(status));
        return;
      }

      for (byte i = 0; i < 16; i++) {
        if (buffer[i] != ' ') {  // ignore spaces
          Serial.write(buffer[i]);
        }
      }
      Serial.println();

    }

    command = "";
  
    }
  
}

  


