# Machine Learning Soccer

## **Table of Contents**
1. [Preface](#Preface)
2. [Component Oriented Programming in Unity](#OP)
3. [Reinforcement Learning using ML-Agent](#DRLMLAgents)
4. [Create a Deep Reinforcement Learning Environment in Unity](#CreateEnv)
    - [Environment Setup (For MacOS with M1 chip)](#EnvSetup)
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

## **Preface<a name="Preface"></a>**

As a soccer enthusiast, I am constantly astonished by the imaginative attacks and dedicated defenses during a high-level match. I wish to replicate such plays in the games; controlling a team and letting it achieve your tactical intentions is tempting. However, due to the physical constrain, we can only handle one player at a time; the off-ball players’ behaviors in the game then become quintessential: the immersive gaming experience vanishes quickly when we notice all kinds of ridiculous, erratic movements and decisions made by the computer-controlled players in the nowadays mainstream soccer games. 

In the past few years, breakthroughs flourished in reinforcement learning. We have witnessed the success of Reinforcement-Learning-based computer programs, such as [AlphaGo](https://www.deepmind.com/research/highlighted-research/alphago) and [OpenAI Five](https://openai.com/five/). By playing against different versions of themselves repeatedly, each time learning from their mistakes, they have become increasingly more robust and better at learning and decision-making, and therefore, being able to defeat professional human players. Naturally, an idea came to my mind: can we apply deep reinforcement learning to the computer-controlled players in soccer games so that they can behave in a less mechanized but more human-like way?

The answer is yes. The Google Brain team has already developed an open-source reinforcement learning environment [Google Research Football](https://github.com/google-research/football). Reinforcement learning works best for scenarios where it is easy to define success but hard to find how to get there. In a soccer game, we can quickly determine success or not by looking at the scoreboard. And it is undoubtedly hard to find a way to score a goal. Unlike conventional soccer games, in which programmers define specific preset strategies for the computer-controlled players to play the game, reinforcement learning provides a soccer environment. It lets them learn the patterns by exploring the environment. 

The idea above sounds simple, but how to implement it is going to be challenging. Consequently, it is wise to break the seemingly impossible mission down into some more achievable smaller steps. Meanwhile, the *Feynman Technique of Learning* suggests that when we want to develop a deep understanding of a topic, we can try to explain it in a straightforward, simple way to others. Therefore, creating this repository allows me to deepen my understanding of relative knowledge in Game Development (Unity) and Machine Learning (Deep Reinforcement Learning). I would also be glad if this repository helps others interested in both areas but struggled at these beginning steps.  

## **Component Oriented Programming in Unity<a name="OP"></a>**

Before diving in, it is essential to highlight some basic concepts of Component Oriented Programming. It is a fundamental and indispensable programming paradigm for game designs using Unity and ML-Agent toolkits. 

In a nutshell, obect-oriented programmingfocuses on the relationships between classes that are combined into one large binary executable, while component-oriented programming focuses on interchangeable code modules that work independently and don't require you to be familiar with their inner workings to use them. 

The fundamental difference between the two methodologies is the way in which they view the final application. In the traditional object-oriented world, even though you may factor the business logic into many fine-grained classes, once those classes are compiled, the result is monolithic binary code. All the classes share the same physical deployment unit, process, address space, security privileges, and so on. If multiple developers work on the same code base, they have to share source files. In such an application, a change made to one class can trigger a massive re-linking of the entire application and necessitate retesting and redeployment of all the other classes.

On the other hand, a component-oriented application comprises a collection of interacting binary application modules —that is, its components and the calls that bind them.

The motivation for breaking down a monolithic application into multiple binary components is analogous to that for placing the code for different classes into different files. By placing the code for each class in an application into its own file, you loosen the coupling between the classes and the developers responsible for them. If you make a change to one class, although you’ll have to re-link the entire application, you’ll only need to recompile the source file for that class.

However, there is more to component-oriented programming than simple software project management. Because a component-based application is a collection of binary building blocks, you can treat its components like LEGO bricks, adding and removing them as you see fit. If you need to modify a component implementation, changes are contained to that component only. No existing client of the component requires recompilation or redeployment. Components can even be updated while a client application is running, as long as the components aren’t currently being used. Improvements, enhancements, and fixes made to a component will immediately be available to all applications that use that component, whether on the same machine or across a network.

A component-oriented application is easier to extend, as well. When you have new requirements to implement, you can provide them in new components, without having to touch existing components not affected by the new requirements.

These factors enable component-oriented programming to reduce the cost of long-term maintenance, a factor essential to almost any business, which helps explain the widespread adoption of component technologies. 

In Unity, we commonly use the component-oriented programming design paradigm. At a high level, components in Unity are C# scripts that can be added onto the GameObject; they can be designed and implemented in many ways. As it has been explained earlier, building up game features using sub-components (rather than inheriting from an object) is usually more efficient and easier to maintain. 

It’s helpful to spend time designing the component architecture of your game because it can reduce overhead time in the long run. Let’s say we want to add a weapon system into a game for players/enemies; we want a sword, a crossbow, and a slingshot.

Using inheritance, you could create a tree for both ranged and non-ranged weapons. You could say that the crossbow and slingshot inherit some of their functionality from the ranged branch since they fire projectiles. But do we really need trees here? You could say that a sword is a ranged weapon, too (with a very low range). In this example, I only have three weapons, but if there were many more, developers could potentially be stuck maintaining several branches of scripts (or a handful of very large ones) for common behavior used across the system.

This is where components save the day! If we divide up the functionality of what any generic weapon can do into sub-components, this problem can be simplified.

Let’s say that any weapon will have a component for damage, range, and projectiles (optional). Using Unity’s UI, you could create a weapon by dragging these components onto any GameObject and customizing the values of each one. Then, if the way projectiles are handled changes further into development, all you need to do is change one script, and all your weapons’ functionalities will be updated instead of just those defined at one level of a tree.

## **Deep Reinforcement Learning using ML-Agents<a name="DRLMLAgents"></a>**
There are a lot of online [tutorials](https://learn.unity.com/project/getting-started?uv=2020.3&courseId=5cf96c41edbc2a2ca6e8810f) and [documentations](https://docs.unity3d.com/Manual/index.html) that would help familiarize yourself with Unity. 

Deep Learning algorithms are based on artificial neural networks, whose algorithmic structures allow models composed of multiple processing layers to learn data representations with various abstraction levels. A typical neural network structure looks like this:

![alt text](https://1.cms.s81c.com/sites/default/files/2021-01-06/ICLH_Diagram_Batch_01_03-DeepNeuralNetwork-WHITEBG.png)

Deep learning is a collection of techniques and methods for using neural networks to solve machine learning tasks, either Supervised Learning, Unsupervised Learning, or Reinforcement Learning.

![alt text](https://miro.medium.com/max/1400/1*qYter-eNfTKyuLg22_5Hvw.webp)

In reinforcement learning there are two core components:

- **Agent**: represents the "solution", which is a computer program with a single role of making decisions (actions) to solve complex decision-making problems under uncertainty.
- **Environment**: represents the "problem", which is everything that comes after the decision of the agent. The environment responds with the consequences of those actions, which are observations or states, and rewards, also sometimes called costs. 

These two core components continuously interact so that the Agent attempts to influence the Environment through actions, and the Environment reacts to the Agent’s actions. 

In the soccer game scenario, each player on the pitch would be the **agent**, while everything that player can observe (ball, gaol, teammates, etc) is included in the **environment**. 

Unity's ML-Agents provide implementations (based on PyTorch) of state-of-the-art algorithms to enable game developers and hobbyists to easily train intelligent agents for 2D, 3D and VR/AR games. Researchers can also use the provided simple-to-use Python API to train Agents using reinforcement learning, imitation learning, neuroevolution, or any other methods.

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

### **Create GameObjects<a name="CreateGameObjects"></a>**

GameObject is the base class for all entities in Unity Scenes. In our simplified environment, we will create four GameObjects: 

**Ground** (represents the soccer field)
1. Right click in Hierarchy window, select 3D Object > Plane.
2. Name the GameObject "Ground".
3. Select the Ground GameObject to view its properties in the Inspector window.
4. Set Transform to Position = `(0, 0, 0)`, Rotation = `(0, 0, 0)`, Scale = `(3, 1, 2)`. 

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/GroundGameObject.png" width=40% height=40%/>

Note: 
- For now, we do not have to pay too much attention on [Mesh Filter](https://docs.unity3d.com/Manual/class-MeshFilter.html), [Mesh Renderer](https://docs.unity3d.com/560/Documentation/Manual/class-MeshRenderer.html), or [Meshe Collider](https://docs.unity3d.com/560/Documentation/Manual/class-MeshCollider.html). They are automatically generated when a GameObject is being created. 

- We can [create and change the material](https://docs.unity3d.com/2019.3/Documentation/Manual/Materials.html) of the GameObjects, because the color code will make our environment more straightforward and visually appealing. 

**Agent** (represents the soccer player)
1. Right click in Hierarchy window, select 3D Object > Cube.
2. Name the GameObject "Agent".
3. Select the Agent Cube to view its properties in the Inspector window.
4. Set Transform to Position = `(13, 0.5, 0)`, Rotation = `(0, 0, 0)`, Scale = `(1, 1, 1)`
5. Add the `Rigidbody` component to the Sphere.

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/AgentGameObject.png" width=40% height=40%/>

Note:
- We set the y value to be 0.5 instead of 0. If we set y-axis to be 0, the Agent Cube would be imbedded in the ground. However, we want it to stand on the ground. Another way to set the Agent cute above the ground is to drag the Agent GameObject toward the ground GameObject while holding the shift key. Then, Unity will automatically place the cube on the ground.

- `Rigidbody` component allows a GameObject to react to real-time physics. This includes reactions to forces and gravity, mass, drag and momentum. We want the Agent to interact with the Ball; therefore, we will add `Rigitbody` to both GameObjects. Notice that we didn't add `Rigidbody` component to the ground GameObject, and that is why when the game start, the ground won't start to fall due to the gravity.

**Target** (represents the ball receiver)
1. Right click in Hierarchy window, select 3D Object > Cube.
2. Name the GameObject "Target".
3. Select the Target Cube to view its properties in the Inspector window.
4. Set Transform to Position = `(-13, 0.5, 0)`, Rotation = `(0, 0, 0)`, Scale = `(1, 1, 1)`

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/TargetGameObject.png" width=40% height=40%/>

**Ball** (represents the soccer ball)
1. Right click in Hierarchy window, select 3D Object > Sphere.
2. Name the GameObject "Ball".
3. Select the Ball sphere to view its properties in the Inspector window.
4. Set Transform to Position = `(0, 0.5, 0)`, Rotation = `(0, 0, 0)`, Scale = `(1, 1, 1)`

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/BallGameObject.png" width=40% height=40%/>

Now the scene should look like the following:

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/Scene.png" width=40% height=40%/>

### **Initializing the Environment<a name="Initialization"></a>**

### **Observing the Environment<a name="Observation"></a>**

### **Taking Actions<a name="Actions"></a>**

### **Assigning Reward<a name="Reward"></a>**

### **Agent Setup<a name="AgentSetup"></a>**

### **Testing<a name="Testing"></a>**

## **What's Next<a name="WhatsNext"></a>**

## **Reference<a name="Reference"></a>**

[Component-Oriented Versus Object-Oriented Programming](https://www.oreilly.com/library/view/programming-net-components/0596102070/ch01s02.html#:~:text=In%20a%20nutshell%2C%20object%2Doriented,inner%20workings%20to%20use%20them.)

[Why You Should Use Component-Based Design in Unity](https://spin.atomicobject.com/2020/09/05/unity-component-based-design/)

[A gentle introduction to Deep Reinforcement Learning](https://towardsdatascience.com/drl-01-a-gentle-introduction-to-deep-reinforcement-learning-405b79866bf4)

[ML-Agents' Github Page](https://github.com/Unity-Technologies/ml-agents)

