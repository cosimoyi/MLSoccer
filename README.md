# Machine Learning Soccer

## **Table of Contents**
1. [Preface](#Preface)
2. [Component Oriented Programming in Unity](#OP)
    - [Object Oriented Programming](#OOP)
    - [Component Oriented Programming](#COP)
    - [Data Orietned Programming](#DOP)
3. [Deep Reinforcement Learning using ML-Agent](#DRLMLAgents)
    - [Deep Learning](#DL)
    - [Reinforcement Learning](#RL)
    - [Deep Reinfocement Learning](#DRL)
    - [ML-Agents Basics](#MLAgents)
4. [Create a Deep Reinforcement Learning Environment in Unity](#CreateEnv)
    - [Environment Setup (For MacOS with M1 chip)](#EnvSetup)
    - [Project Structure](#Structure)
    - [Create GameObjects](#CreateGameObjects)
    - [Initializing the Environment](#Initialization)
    - [Observing the Environment](Observation)
    - [Taking Actions](#Actions)
    - [Assigning Reward](#Reward)
    - [Agent Setup](#AgentSetup)
    - [Testing](#Test)
    - [Training](#Train)
5. [What's Next](#WhatsNext)
6. [Reference](#Reference)


$-$

## **Preface**

As a soccer enthusiast, I am constantly astonished by the imaginative attacks and dedicated defenses during a high-level match. I wish to replicate such plays in the games; controlling a team and letting it achieve your tactical intentions is tempting. However, due to the physical constrain, we can only handle one player at a time; the off-ball players’ behaviors in the game then become quintessential: the immersive gaming experience vanishes quickly when we notice all kinds of ridiculous, erratic movements and decisions made by the computer-controlled players in the nowadays mainstream soccer games. 

In the past few years, breakthroughs flourished in reinforcement learning. We have witnessed the success of Reinforcement-Learning-based computer programs, such as [AlphaGo](https://www.deepmind.com/research/highlighted-research/alphago) and [OpenAI Five](https://openai.com/five/). By playing against different versions of themselves repeatedly, each time learning from their mistakes, they have become increasingly more robust and better at learning and decision-making, and therefore, being able to defeat professional human players. Naturally, an idea came to my mind: can we apply deep reinforcement learning to the computer-controlled players in soccer games so that they can behave in a less mechanized but more human-like way?

The answer is yes. The Google Brain team has already developed an open-source reinforcement learning environment [Google Research Football](https://github.com/google-research/football). Reinforcement learning works best for scenarios where it is easy to define success but hard to find how to get there. In a soccer game, we can quickly determine success or not by looking at the scoreboard. And it is undoubtedly hard to find a way to score a goal. Unlike conventional soccer games, in which programmers define specific preset strategies for the computer-controlled players to play the game, reinforcement learning provides a soccer environment. It lets them learn the patterns by exploring the environment. 

The idea above sounds simple, but how to implement it is going to be challenging. Consequently, it is wise to break the seemingly impossible mission down into some more achievable smaller steps. Meanwhile, the *Feynman Technique of Learning* suggests that when we want to develop a deep understanding of a topic, we can try to explain it in a straightforward, simple way to others. Therefore, creating this repository allows me to deepen my understanding of relative knowledge in Game Development (Unity) and Machine Learning (Deep Reinforcement Learning). I would also be glad if this repository helps others interested in both areas but struggled at these beginning steps.  

## **Component Oriented Programming in Unity<a name="OP"></a>**

Before diving in, it is essential to highlight some basic concepts of Component Oriented Programming. It is a fundamental and indispensable programming paradigm for game designs using Unity and ML-Agent toolkits. 

### **Object Oriented Programming<a name="OOP"></a>**

$-$

### **Component Oriented Programming<a name="COP"></a>**

$-$

### **Data Oriented Programming<a name="DOP"></a>**

$-$

## **Deep Reinforcement Learning using ML-Agents<a name="DRLMLAgents"></a>**
There are a lot of online [tutorials](https://learn.unity.com/project/getting-started?uv=2020.3&courseId=5cf96c41edbc2a2ca6e8810f) and [documentations](https://docs.unity3d.com/Manual/index.html) that would help familiarize yourself with Unity. In this section, I only focus on the concepts related to this specific repository. 

More information will be updated in the future. 

### **Deep Learning<a name="DL"></a>**

$-$

### **Reinforcement Learning<a name="RL"></a>**

$-$

### **Deep Reinforcement Learning<a name="DRL"></a>**

$-$

### **ML-Agents Basics<a name="MLAgents"></a>**

$-$

## **Create a Deep Reinforcement Learning Environment in Unity<a name="CreateEnv"></a>**

### **Environment Setup<a name="EnvSetup"></a>**

If you decide to set up the ML-Agents environment in a Windows device or a macOS device that’s not running on the M1 chip, then I believe the [official tutorial](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Installation.md) would be sufficient for the setup. 

However, we need to do something slightly different for a macOS device with an M1 chip to ensure everything works fine without compatibility issues. After **creating the project**, **downloading the ML-Agents package** in Unity Editor, and **having Miniconda or Anaconda installed** in our computer, we can set up the python virtual environment in the following way:

1. Open a terminal and areate an empty virtual environment
```
conda create -n mlenv
```
2. Activate the virtual environment we just created
```
conda activate mlenv
```
3. Use x86_64 architecture channel(s)
```
conda config --env --set subdir osx-64
```
4. Install python 3.7
```
conda install python=3.7 numpy
```
5. Install pytorch, torchvision, and torchaudio (optional) 
```
conda install pytorch==1.12.0 torchvision==0.13.0 torchaudio==0.12.0 -c pytorch
```
6. Install ml-agents *(Note: don't forget to navigate to the directory that contains the ml-agents package)*
```
pip install -e ./ml-agents-envs
pip install -e ./ml-agents
```
7. Install Importliberalisierungen-metadata 4.4
```
pip install importlib-metadata==4.4
```

### **Project Structure<a name="Structure"></a>**