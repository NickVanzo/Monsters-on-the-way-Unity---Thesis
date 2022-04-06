interface IEnemy
{
    RewardType GiveReward();
    void Attack();
    void Move();    
    void Die();
    void TakeDamage(int dmg);
    void Cure(int qnt);
}
