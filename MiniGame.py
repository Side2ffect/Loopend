import pygame
import os
import random
pygame.font.init()

pygame.init()

size = [600, 480]
screen = pygame.display.set_mode(size)
pygame.display.set_caption("L\u221ePEND")

WHITE = (255,255,255)
BLACK = (0, 0, 0)
RED = (255, 0, 0)
LeftBorder = pygame.Rect(0, 0, 10, 480)
RightBorder = pygame.Rect(590, 0, 10, 480)
UpBorder = pygame.Rect(0, 0, 600, 10)
BottomBorder = pygame.Rect(0, 470, 600, 10)
Borders = [LeftBorder, RightBorder, UpBorder, BottomBorder]

ScoreFont = pygame.font.SysFont('arial', 25)
GameOverFont = pygame.font.SysFont('arial', 100)
MenuFont = pygame.font.SysFont('arial', 150)
ResumeFont = pygame.font.SysFont('arial', 50)

player_width = 45
player_height = 45
speed = 3
gravity = 2

backgroundMenu = pygame.image.load(os.path.join('Image', 'Universe.png'))
backgroundMenu = pygame.transform.scale(backgroundMenu, size)

background = pygame.image.load(os.path.join('Image', 'Darker.png'))
background = pygame.transform.scale(background, size)

player = pygame.image.load(os.path.join('Image', 'player.png'))
player = pygame.transform.scale(player, (player_width, player_height))

meteorImage = pygame.image.load(os.path.join('Image', 'Meteor.png'))
meteorImage = pygame.transform.scale(meteorImage, (player_width, player_height))

def mainMenu():
    while True:
        screen.blit(backgroundMenu, (0,0))
        MenuText = MenuFont.render("L\u221ePEND", 1, RED)
        ResumeText = ResumeFont.render("Press Spacebar to play", 1, WHITE)
        screen.blit(MenuText, (300 - MenuText.get_width()/2, 180 - MenuText.get_height()/2))
        screen.blit(ResumeText, (300 - ResumeText.get_width()/2, 380 - ResumeText.get_height()/2))

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
             
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_SPACE:
                    startGame()

        pygame.display.update()


def playerMovement(movingPlayer, playerCharacter):
    if movingPlayer[pygame.K_UP]:
        playerCharacter.y -= speed
    if movingPlayer[pygame.K_DOWN]:
        playerCharacter.y += speed
    if movingPlayer[pygame.K_LEFT]:
        playerCharacter.x -= speed
    if movingPlayer[pygame.K_RIGHT]:
        playerCharacter.x += speed 

def upGravity(playerCharacter):
    playerCharacter.y -= gravity
    pygame.draw.rect(screen, RED, UpBorder)

def downGravity(playerCharacter):
    playerCharacter.y += gravity
    pygame.draw.rect(screen, RED, BottomBorder)

def leftGravity(playerCharacter):
    playerCharacter.x -= gravity
    pygame.draw.rect(screen, RED, LeftBorder)

def rightGravity(playerCharacter):
    playerCharacter.x += gravity
    pygame.draw.rect(screen, RED, RightBorder)

def settingGravity(randomNumber, playerCharacter):
    if randomNumber == 1:
        upGravity(playerCharacter)
    elif randomNumber == 2:
        downGravity(playerCharacter)
    elif randomNumber == 3:
        leftGravity(playerCharacter)
    elif randomNumber == 4:
        rightGravity(playerCharacter)

def ResetBorderColor():
    for border in Borders:
        pygame.draw.rect(screen, BLACK, border)

def pauseGame():
    pause = True
    pauseText = GameOverFont.render("Paused", 1, BLACK)
    reesumeText = ScoreFont.render("Press spacebar to resume", 1, BLACK)

    while pause:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
            
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_SPACE:
                    pause = False

        screen.fill(WHITE)
        screen.blit(pauseText, (300 - pauseText.get_width()/2, 240 - pauseText.get_height()/2))
        screen.blit(reesumeText, (300 - reesumeText.get_width()/2, 320 - reesumeText.get_height()/2))
        pygame.display.update()


def drawEndScreen(score):
    gameoverText = GameOverFont.render("Game Over", 1, WHITE)
    screen.blit(gameoverText, (300 - gameoverText.get_width()/2, 220 - gameoverText.get_height()/2))
    scoreText = ResumeFont.render("Score: " + str(score), 1, WHITE)
    screen.blit(scoreText, (300 - scoreText.get_width()/2, 300 - scoreText.get_height()/2))
    pygame.display.update()
    pygame.time.delay(1500)

def startGame():
    gamePlay = True
    clock = pygame.time.Clock()
    time = 300
    score = 0
    randomNumber = 0
    meteors = []
    
    for i in range(7):
        stone = pygame.Rect(meteorImage.get_rect())
        stone.left = 600
        stone.top = random.randint(player_height, 480 - player_height)
        meteorSpeed = random.randint(2, 3)
        meteors.append({'rect': stone, 'dy': meteorSpeed})

    playerCharacter = pygame.Rect(150, 200, player_width, player_height)

    while gamePlay:
        clock.tick(60)
        screen.fill(WHITE)
        screen.blit(background, (0,0))
        ResetBorderColor()

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                gamePlay = False
                pygame.quit()
             
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_p:
                    pauseGame()

        for meteor in meteors:
            meteor['rect'].left -= meteor['dy']
            if meteor['rect'].left < 0:
                meteors.remove(meteor)
                stone = pygame.Rect(meteorImage.get_rect())
                stone.left = 600
                stone.top = random.randint(player_height, 480 - player_height)
                meteorSpeed = random.randint(2, 5)
                meteors.append({'rect': stone, 'dy': meteorSpeed})

        for border in Borders:
            if border.colliderect(playerCharacter):
                drawEndScreen(score)
                gamePlay = False
                break

        for meteor in meteors:
            if meteor['rect'].colliderect(playerCharacter):
                meteors.remove(meteor)
                drawEndScreen(score)
                gamePlay = False
                break
            screen.blit(meteorImage, meteor['rect'])

        time -= 1
        if time <= 0:
            ResetBorderColor()
            time = 300
            randomNumber= random.randint(0, 4)

        score += 1

        settingGravity(randomNumber, playerCharacter)
        movingPlayer = pygame.key.get_pressed()   
        playerMovement(movingPlayer, playerCharacter)
        screen.blit(player, (playerCharacter.x, playerCharacter.y))

        scoreText = ScoreFont.render("Score: " + str(score), 1, WHITE)
        screen.blit(scoreText, (10, 10))
        pygame.display.update()
    
    startGame()

if __name__ == "__main__":
    mainMenu()