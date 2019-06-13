using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class OpenWorldGameEditorTests
  {
    // A Test behaves as an ordinary method
    [Test]
    public void Player_Exists_By_Name()
    {
      // Use the Assert class to test conditions
      var player = GameObject.Find("Player");

      Assert.IsTrue(player != null);
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void Player_Exists_By_Type()
    {
      // Use the Assert class to test conditions
      var player = GameObject.FindObjectOfType<Player>();

      Assert.IsTrue(player != null);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Player_Was_Damaged_By_Something()
    {
      // Detecting the player at runtime
      var enemy = GameObject.FindObjectOfType<Enemy>();

      // Recording the health of the player
      var oldHealth = enemy.health;

      // Waiting one second
      yield return null;

      // Asserting that the player's health should be changed
      Assert.IsTrue(enemy.health != oldHealth);
    }
  }
}
