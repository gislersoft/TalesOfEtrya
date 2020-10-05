import tensorflow as tf
import math
import os
from a3c.model import AC_Network
from a3c.Worker import *
from unityagents.environment import UnityEnvironment

from time import sleep
import threading
import multiprocessing

max_steps = 5e5
run_path = "a3c"
env_name = "ss2"

#Algorithm specific parameters
gamma = 0.99
buffer_size = 30
learning_rate = 1e-4


#ML agents
env = UnityEnvironment(file_name=env_name)
print(str(env))
brain_names = env.external_brain_names[:]
aradis_brain = env.brains[brain_names[0]]

tf.reset_default_graph()
model_path = "./model/{}".format(str(run_path))
if not os.path.exists(model_path):
    print("Creating model path")
    os.makedirs(model_path)
#Create worker threads
with tf.device("/cpu:0"):
    global_episodes = tf.Variable(0, dtype=tf.int32, name='global_episodes', trainable=False)
    trainer = tf.train.AdamOptimizer(learning_rate)
    o_height, o_width = aradis_brain.camera_resolutions[0]['height'], aradis_brain.camera_resolutions[0]['width'], 
    target_network = AC_Network(o_height, o_width, 1, o_height * o_width, aradis_brain.action_space_size, trainer, 'global', tf.nn.relu)

    env.close()

    num_workers = 1#math.floor(multiprocessing.cpu_count() / 2)
    workers = [None] * num_workers
    print("Starting %d environment threads" % num_workers)
    envs = []
    for i in range(num_workers):
        env = UnityEnvironment(file_name=env_name, worker_id=i)
        envs.append(env)
        brain_names = env.external_brain_names[:]
        aradis_brain = env.brains[brain_names[0]]
        workers[i] = Worker(env, aradis_brain, i, o_height * o_width, aradis_brain.action_space_size, model_path, trainer, global_episodes)
        
    saver = tf.train.Saver(max_to_keep=5)

with tf.Session() as sess:
    coord = tf.train.Coordinator()

    sess.run(tf.global_variables_initializer())

    #Start the worker threads
    worker_threads = []
    for worker in workers:
        worker_work = lambda: worker.work(max_steps, buffer_size, gamma, sess, coord, saver)
        t = threading.Thread(target=(worker_work))
        t.start()
        sleep(0.5)
        worker_threads.append(t)
    coord.join(worker_threads)

for env in envs:
    env.close()
