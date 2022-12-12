# Machine Learning Soccer

## **Table of Contents**
1. [Preface](#Preface)
2. [Component Oriented Programming in Unity](#COP)
3. [Reinforcement Learning using ML-Agent](#RLMLAgents)
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

## **Component Oriented Programming in Unity<a name="COP"></a>**

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

## **Deep Reinforcement Learning using ML-Agents<a name="RLMLAgents"></a>**
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

After creating the GameObjects for the environment, now we need to group them into a training field:
1. Right-click on your Project Hierarchy and create a new empty GameObject. Name it TrainingField.
2. Reset the TrainingArea’s Transform so that it is at `(0,0,0)` with Rotation `(0,0,0)` and Scale `(1,1,1)`.
3. Drag the Ground, Target, Agent, and Ball GameObjects in the Hierarchy into the TrainingArea GameObject.

Later, we can duplicate these traning fields to make them train simultaneously, so that the training process will become significantly faster comparing to only having one training field. 

Now the scene should look like the following:

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/Scene.png" width=80% height=80%/>

And the Hierarchy would look like this:

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/Hierarchy.png" width=40% height=40%/>

### **Initializing the Environment<a name="Initialization"></a>**

After creating the GameObjects for this environment, we need to initialize the training environment. To achieve that, we can create a Script that attaches to the Agent of this environment:
1. Select the Agent GameObject to view it in the Inspector window.
2. Click Add Component.
3. Click New Script in the list of components (at the bottom).
4. Name the script "RollerAgent".
5. Click Create and Add.

Then, edit the `Agent` script:
1. In the Unity Project window, double-click the Agent script to open it in your code editor.
2. Import ML-Agent package by adding
~~~c#
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
~~~
3. Delete `Update()` function since we are not using it, but keep `Start()` function. `Start()` function is called on the frame when a script is enabled just before any of the Update methods are called the first time. 

The process of training in the ML-Agents Toolkit involves running episodes where the Agent attempts to solve the task. In our case, we want the Agent push the Ball toward the Target. Each episode lasts until the Agents solves the task (the Ball reaches the Target), fails (rolls off the platform) or times out (takes too long to solve or fail at the task). 

At the start of each episode, OnEpisodeBegin() is called to set-up the environment for a new episode. Typically the scene is initialized in a random manner to enable the agent to learn to solve the task under a variety of conditions. 

In this example, each time the Agent "dribble" the Ball and let the Ball reaches the target, the episode ends and then both the Target and the Agent are moved to a new random location; and if the Ball or Agent fall off the Ground, they will be put back onto the floor. All the aforesaid functionalities are handled in OnEpisodeBegin().

To interact with the Target and the Ball, we need two references to their Transform (which stores a GameObject's position, orientation and scale in the 3D world). To get this reference, add public field of type Transform to the Agent class and Ball class. Public fields of a component in Unity get displayed in the Inspector window, allowing you to choose which GameObject to use as the target in the Unity Editor.

To reset the Agent and Ball's velocity (and later to apply force to move the agent) we need references to the Rigidbody component. A Rigidbody is Unity's primary element for physics simulation. Since the Rigidbody component is on the same GameObject as our Agent script, the best way to get this reference is using GameObject.GetComponent<T>(), which we can call in our script's Start() method. 

So far, our RollerAgent script looks like:

~~~c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentController : Agent
{
    Rigidbody rBodyAgent; 
    Rigidbody rBodyBall;

    void Start() 
    {
        rBodyAgent = GetComponent<Rigidbody>();
        rBodyBall = Ball.GetComponent<Rigidbody>();
    }

    public Transform Target;  // the reference for Target
    public Transform Ball;  // the reference for Ball

    public override void OnEpisodeBegin()
    {
        // If either the agent or the ball fell, zero its momentum
        if (this.transform.localPosition.y < 0 || Ball.localPosition.y < 0)  
        {
            this.rBodyAgent.angularVelocity = Vector3.zero;  
            this.rBodyAgent.velocity = Vector3.zero;  

            rBodyBall.angularVelocity = Vector3.zero;
            rBodyBall.velocity = Vector3.zero;
        }

        // Move the Target and the Agent to a new spot, reset ball to the center point
        this.transform.localPosition = new Vector3(Random.Range(3.0f, 13.0f),
                                                   0.5f,
                                                   Random.Range(-8.0f, 8.0f));
        this.transform.LookAt(Ball);

        Target.localPosition = new Vector3(Random.Range(-13.0f, -3.0f),
                                           0.5f,
                                           Random.Range(-8.0f, 8.0f));
        Ball.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
}
~~~

### **Observing the Environment<a name="Observation"></a>**

The Agent sends the information we collect to the Brain, which uses it to make a decision. When you train the Agent (or use a trained model), the data is fed into a neural network as a feature vector. For an Agent to successfully learn a task, we need to provide the correct information. A good rule of thumb for deciding what information to collect is to consider what you would need to calculate an analytical solution to the problem.

In our case, the information our Agent collects includes the position of the Target and the Ball, the position of the Agent itself, and the velocity of the Agent and the Ball. This helps the Agent learn to control its speed so it doesn't overshoot the target and roll off the platform. In total, the agent observation contains 11 values as implemented below:

~~~c#
public override void CollectObservations(VectorSensor sensor)
{
    // Target, Ball, and Agent Positions
    sensor.AddObservation(Target.localPosition);  // x, y, z
    sensor.AddObservation(Ball.localPosition);  // x, y, z
    sensor.AddObservation(this.transform.localPosition);  // x, y, z

    // Agent Velocity
    sensor.AddObservation(rBodyAgent.velocity.x);  
    sensor.AddObservation(rBodyAgent.velocity.z);
}
~~~

### **Taking Actions<a name="Actions"></a>**

The final part of the Agent code is the `Agent.OnActionReceived()` method, which receives actions and assigns the reward.

To solve our task of letting the Agent push the Ball towards the Target, the Agent needs to be able to move in the x and z directions and rotate along the y axis. As such, the Agent needs 3 actions: the first determines the movement applied along the x-axis; the second determines the movement applied along the z-axis; and the third determins the rotation applied along the y-axis. (We can also approach the task by adding forces to the Agent).

The Agent applies the values from the `action[]` array to its Rigidbody component `rBody`, using `Rigidbody.AddForce()`:

~~~c#
// Actions for moving and rotating
public float speedMultiplier = 10f;
Vector3 controlSignal = Vector3.zero;
Vector3 rotateDir = Vector3.zero;

controlSignal.x = actions.ContinuousActions[0];
controlSignal.z = actions.ContinuousActions[1];
rotateDir.y = actions.ContinuousActions[2];

transform.Translate(controlSignal * Time.deltaTime * speedMultiplier);
transform.Rotate(rotateDir, Time.deltaTime * 100f);
~~~

### **Assigning Reward<a name="Reward"></a>**

Reinforcement learning requires rewards to signal which decisions are good and which are bad. The learning algorithm uses the rewards to determine whether it is giving the Agent the optimal actions. You want to reward an Agent for completing the assigned task. In this case, the Agent is given a reward of 1.0 for reaching the Target cube.

Rewards are assigned in `OnActionReceived()`. The Agent calculates the distance to detect when it reaches the target. When it does, the code calls `Agent.SetReward()` to assign a reward of 1.0 and marks the agent as finished by calling `EndEpisode()` on the Agent.

~~~c#
// Rewards
float distanceToTarget = Vector3.Distance(Ball.localPosition, Target.localPosition);
float distanceToBall = Vector3.Distance(Ball.localPosition, this.transform.localPosition);

if (distanceToBall < 1.42f)  // When the distance between Ball and Agent is really close, assign a reward of 0.05 (So the Agent would prefer dribble than kicking the ball)
{
    SetReward(0.05f);
}

if (distanceToTarget < 1.42f)  // When the distance between Ball and Target is really close, assign a reward of 1.0
{
    SetReward(1.0f);
    EndEpisode();
}
else if (this.transform.localPosition.y < 0)  // When the Agent falls, end episode
{
    EndEpisode();
}
else if (Ball.localPosition.y < 0)  // When the Ball falls, end episode
{
    EndEpisode();
}
~~~

With the action and reward logic outlined above, the final version of   `OnActionReceived()` looks like:

~~~c#
public float speedMultiplier = 10;
public override void OnActionReceived(ActionBuffers actions)
{
    // Actions for moving and rotating
    Vector3 controlSignal = Vector3.zero;
    Vector3 rotateDir = Vector3.zero;

    controlSignal.x = actions.ContinuousActions[0];
    controlSignal.z = actions.ContinuousActions[1];
    rotateDir.y = actions.ContinuousActions[2];

    transform.Translate(controlSignal * Time.deltaTime * speedMultiplier);
    transform.Rotate(rotateDir, Time.deltaTime * 100f);

    // Rewards
    float distanceToTarget = Vector3.Distance(Ball.localPosition, Target.localPosition);
    float distanceToBall = Vector3.Distance(Ball.localPosition, this.transform.localPosition);

    if (distanceToBall < 1.42f)
    {
        SetReward(0.05f);
    }

    if (distanceToTarget < 1.42f)
    {
        SetReward(1.0f);
        EndEpisode();
    }
    else if (this.transform.localPosition.y < 0)
    {
        EndEpisode();
    }
    else if (Ball.localPosition.y < 0)
    {
        EndEpisode();
    }
}
~~~

### **Agent Setup<a name="AgentSetup"></a>**

Now that all the GameObjects and ML-Agent components are in place, it is time to connect everything together in the Unity Editor. This involves adding and setting some of the Agent Component's properties so that they are compatible with our Agent script.

1. Select the Agent GameObject to show its properties in the Inspector window.
2. Drag the Target and Ball GameObject in the Hierarchy into the Target field and Ball field in Agent Script.
3. Add a Decision Requester script with the Add Component button. Set the Decision Period to 10. For more information on decisions, see the Agent documentation
4. Add a Behavior Parameters script with the Add Component button. Set the Behavior Parameters of the Agent to the following:
    - Behavior Name: SoccerBall
    - Vector Observation > Space Size = 11
    - Actions > Continuous Actions = 3

In the inspector, the Agent should look like this now:

<img src="https://github.com/cosimoyi/MLSoccer/blob/main/img/AgentSetup.png" width=40% height=40%/>

### **Testing<a name="Test"></a>**

It is always a good idea to first test your environment by controlling the Agent using the keyboard. To do so, you will need to extend the `Heuristic()` method in the `Agent` class. For our example, the heuristic will generate an action corresponding to the values of the "Horizontal" and "Vertical" input axis (which correspond to the keyboard arrow keys):

~~~c#
public override void Heuristic(in ActionBuffers actionsOut)
{
    var continuousActionsOut = actionsOut.ContinuousActions;
    if (Input.GetKey(KeyCode.W))
    {
        continuousActionsOut[0] = -1;
    }
    if (Input.GetKey(KeyCode.S))
    {
        continuousActionsOut[0] = 1;
    }
    if (Input.GetKey(KeyCode.A))
    {
        continuousActionsOut[1] = -1;
    }
    if (Input.GetKey(KeyCode.D))
    {
        continuousActionsOut[1] = 1;
    }
    if (Input.GetKey(KeyCode.E))
    {
        continuousActionsOut[2] = 1;
    }
    if (Input.GetKey(KeyCode.Q))
    {
        continuousActionsOut[2] = -1;
    }
}
~~~

## **Training the Envornmnet<a name="Train"></a>**

The hyperparameters for training are specified in a configuration file that you pass to the mlagents-learn program. Create a new `soccerball_config.yaml` file under `config/` and include the following hyperparameter values:

~~~yaml
behaviors:
  SoccerBall:
    trainer_type: ppo
    hyperparameters:
      batch_size: 10
      buffer_size: 100
      learning_rate: 3.0e-4
      beta: 5.0e-4
      epsilon: 0.2
      lambd: 0.99
      num_epoch: 3
      learning_rate_schedule: linear
      beta_schedule: constant
      epsilon_schedule: linear
    network_settings:
      normalize: false
      hidden_units: 128
      num_layers: 2
    reward_signals:
      extrinsic:
        gamma: 0.99
        strength: 1.0
    max_steps: 500000
    time_horizon: 64
    summary_freq: 10000
~~~

Since this example creates a very simple training environment with only a few inputs and outputs, using small batch and buffer sizes speeds up the training considerably. However, if you add more complexity to the environment or change the reward or observation functions, you might also find that training performs better with different hyperparameter values. In addition to setting these hyperparameter values, the Agent DecisionFrequency parameter has a large effect on training time and success. A larger value reduces the number of decisions the training algorithm has to consider and, in this simple environment, speeds up training.

To train your agent, run the following command before pressing Play in the Editor:

~~~
mlagents-learn config/rollerball_config.yaml --run-id=RollerBall
~~~

To monitor the statistics of Agent performance during training, use [TensorBoard](https://github.com/Unity-Technologies/ml-agents/blob/release_19_docs/docs/Using-Tensorboard.md).

## **What's Next<a name="WhatsNext"></a>**

- Create Multiple Training Areas to Speed Up the Training Process.
- Design Different Reward Functions to Train the Agent to Achieve more Tasks.
- ...

## **Reference<a name="Reference"></a>**

This page is adapted from [this](https://github.com/Unity-Technologies/ml-agents/blob/release_19_docs/docs/Learning-Environment-Create-New.md) official tutorial, other sources include:

[Component-Oriented Versus Object-Oriented Programming](https://www.oreilly.com/library/view/programming-net-components/0596102070/ch01s02.html#:~:text=In%20a%20nutshell%2C%20object%2Doriented,inner%20workings%20to%20use%20them.)

[Why You Should Use Component-Based Design in Unity](https://spin.atomicobject.com/2020/09/05/unity-component-based-design/)

[A gentle introduction to Deep Reinforcement Learning](https://towardsdatascience.com/drl-01-a-gentle-introduction-to-deep-reinforcement-learning-405b79866bf4)

[ML-Agents' Github Page](https://github.com/Unity-Technologies/ml-agents)

