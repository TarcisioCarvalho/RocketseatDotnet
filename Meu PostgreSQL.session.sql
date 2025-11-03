-- Criação dos tipos ENUM
CREATE TYPE payment_method_enum AS ENUM ('Cartão', 'Dinheiro', 'Pix', 'Outro');
CREATE TYPE status_enum AS ENUM ('Pago', 'Cancelado');

SELECT * FROM faturamento
-- Criação da tabela de faturamento
CREATE TABLE faturamento (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    date DATE NOT NULL,
    barber_name VARCHAR(80) NOT NULL CHECK (CHAR_LENGTH(barber_name) BETWEEN 2 AND 80),
    client_name VARCHAR(120) NOT NULL CHECK (CHAR_LENGTH(client_name) BETWEEN 2 AND 120),
    service_name VARCHAR(120) NOT NULL CHECK (CHAR_LENGTH(service_name) BETWEEN 2 AND 120),
    amount DECIMAL(10, 2) NOT NULL CHECK (amount >= 0),
    payment_method payment_method_enum NOT NULL,
    status status_enum NOT NULL,
    notes VARCHAR(500),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Regra: se status = 'Cancelado', amount deve ser 0
    CONSTRAINT check_canceled_amount CHECK (
        (status = 'Cancelado' AND amount = 0) OR (status != 'Cancelado')
    )
);

-- Função para atualizar automaticamente o updated_at
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger para atualizar updated_at automaticamente
CREATE TRIGGER set_updated_at
    BEFORE UPDATE ON faturamento
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

-- Índices para melhorar performance nas consultas
CREATE INDEX idx_faturamento_date ON faturamento(date);
CREATE INDEX idx_faturamento_barber_name ON faturamento(barber_name);
CREATE INDEX idx_faturamento_status ON faturamento(status);
CREATE INDEX idx_faturamento_created_at ON faturamento(created_at DESC);

-- Comentários descritivos
COMMENT ON TABLE faturamento IS 'Registros de faturamento da barbearia';
COMMENT ON COLUMN faturamento.id IS 'GUID gerado automaticamente';
COMMENT ON COLUMN faturamento.date IS 'Data do faturamento';
COMMENT ON COLUMN faturamento.barber_name IS 'Nome do barbeiro (2-80 caracteres)';
COMMENT ON COLUMN faturamento.client_name IS 'Nome do cliente (2-120 caracteres)';
COMMENT ON COLUMN faturamento.service_name IS 'Nome do serviço: corte, barba, combo (2-120 caracteres)';
COMMENT ON COLUMN faturamento.amount IS 'Valor do serviço (≥0, deve ser 0 se cancelado)';
COMMENT ON COLUMN faturamento.payment_method IS 'Forma de pagamento';
COMMENT ON COLUMN faturamento.status IS 'Status do faturamento';
COMMENT ON COLUMN faturamento.notes IS 'Observações adicionais (até 500 caracteres)';
COMMENT ON COLUMN faturamento.created_at IS 'Data/hora de criação do registro';
COMMENT ON COLUMN faturamento.updated_at IS 'Data/hora da última atualização';



ALTER TABLE faturamento RENAME TO billings;



-- PASSO 1: Dropar tudo
DROP TABLE IF EXISTS billings CASCADE;
DROP TYPE IF EXISTS payment_method_enum CASCADE;
DROP TYPE IF EXISTS status_enum CASCADE;
DROP FUNCTION IF EXISTS update_updated_at_column() CASCADE;

-- PASSO 2: Criar tipos ENUM
CREATE TYPE payment_method_enum AS ENUM ('Credit_Card', 'Cash', 'Pix', 'Other');
CREATE TYPE status_enum AS ENUM ('Paid', 'Canceled');

-- PASSO 3: Criar tabela (simples e direto)
CREATE TABLE billings (
    id UUID PRIMARY KEY,
    date DATE NOT NULL,
    barber_name VARCHAR(80) NOT NULL CHECK (CHAR_LENGTH(barber_name) BETWEEN 2 AND 80),
    client_name VARCHAR(120) NOT NULL CHECK (CHAR_LENGTH(client_name) BETWEEN 2 AND 120),
    service_name VARCHAR(120) NOT NULL CHECK (CHAR_LENGTH(service_name) BETWEEN 2 AND 120),
    amount DECIMAL(10, 2) NOT NULL CHECK (amount >= 0),
    payment_method payment_method_enum NOT NULL,
    status status_enum NOT NULL,
    notes VARCHAR(500),
    created_at TIMESTAMP NOT NULL,
    updated_at TIMESTAMP NOT NULL,
    
    CONSTRAINT check_canceled_amount CHECK (
        (status = 'Canceled' AND amount = 0) OR (status != 'Canceled')
    )
);
ALTER TABLE billings ALTER COLUMN updated_at DROP NOT NULL;

-- CRIAÇÃO DA TABELA USERS
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(120) NOT NULL CHECK (CHAR_LENGTH(name) BETWEEN 2 AND 120),
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Trigger para atualizar updated_at automaticamente na tabela users
CREATE TRIGGER set_updated_at_users
    BEFORE UPDATE ON users
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

-- Índices para melhorar performance nas consultas de users
CREATE INDEX idx_users_email ON users(email);
CREATE INDEX idx_users_created_at ON users(created_at DESC);

-- Comentários descritivos para tabela users
COMMENT ON TABLE users IS 'Tabela de usuários do sistema';
COMMENT ON COLUMN users.id IS 'GUID gerado automaticamente';
COMMENT ON COLUMN users.name IS 'Nome do usuário (2-120 caracteres)';
COMMENT ON COLUMN users.email IS 'Email único do usuário';
COMMENT ON COLUMN users.password IS 'Senha criptografada do usuário';
COMMENT ON COLUMN users.created_at IS 'Data/hora de criação do usuário';
COMMENT ON COLUMN users.updated_at IS 'Data/hora da última atualização';

-- ADICIONAR RELACIONAMENTO NA TABELA BILLINGS
ALTER TABLE billings ADD COLUMN user_id UUID;

-- Criar Foreign Key para relacionar billings com users
ALTER TABLE billings
    ADD CONSTRAINT fk_billings_user
    FOREIGN KEY (user_id)
    REFERENCES users(id)
    ON DELETE SET NULL
    ON UPDATE CASCADE;

-- Criar índice para melhorar performance nas consultas por user_id
CREATE INDEX idx_billings_user_id ON billings(user_id);

-- Comentário descritivo para a nova coluna
COMMENT ON COLUMN billings.user_id IS 'ID do usuário responsável pelo registro (FK para users)';

SELECT * From billings;
 SELECT
     id,
     date,
     barber_name,
     client_name,
     service_name,
     amount,
     payment_method,
     status,
     notes,
     created_at,
     updated_at,
     user_id
 FROM billings
 WHERE id = 'e481c575-ef15-40d2-9af2-54da0ee878b5'